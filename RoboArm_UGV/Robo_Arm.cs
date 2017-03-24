using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dynamixel_sdk;

namespace RoboArm_UGV
{
    class Robo_Arm
    {
        #region Private Constant Properties
        private const Int16 Base_ID = 1;
        private const Int16 Shoulder_ID = 2;
        private const Int16 Elbow_ID = 3;
        private const Int16 Base_Start = -400;                  // start position of the Base servo
        private const Int16 Shoulder_Start = 700;               // start position of the Shoulder servo
        private const Int16 Elbow_Start = 500;                  // start position of the Elbow servo
        private const short x_centergoal = 100;                 // goal for the x-coordinate center of the payload in pixels
        private const short y_centergoal = 200;                 // goal for the y-coordinate center of the payload in pixels
        private const int arm_payload_threshold = 100;              // measured in inches; can be changed to meters
        private const int ugv_payload_threshold = 100;
        #endregion
        #region Changing Properties
        private double error_x = 0;                             // error from Base PID
        private double error_y = 0;                             // error from Elbow PID
        private TimeSpan time;                                  // elapsed time for PID calculations
        private bool atPayload = false;
        #endregion
        #region DXL Servos and PIDs
        public DXL_Servo Base;
        public DXL_Servo Shoulder;
        public DXL_Servo Elbow;

        public PID_Contoller basePID;
        public PID_Contoller elbowPID;
        public PID_Contoller shoulderPID;
        #endregion

        public Robo_Arm()
        {
            Base = new DXL_Servo(Base_ID, -400, 1000);
            Shoulder = new DXL_Servo(Shoulder_ID, -400, 1000);
            Elbow = new DXL_Servo(Elbow_ID, -400, 1000);
            armStartPosition();
            basePID = new PID_Contoller(0, 0, 0, Base.DXL_MAXIMUM_POSITION_VALUE,Base.DXL_MINIMUM_POSITION_VALUE);
            shoulderPID = new PID_Contoller(0, 0, 0, Shoulder.DXL_MAXIMUM_POSITION_VALUE, Shoulder.DXL_MINIMUM_POSITION_VALUE);
        }

        /// <summary>
        /// Driver function to test class methods
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //PID_Contoller elbow_pi = new PID_Contoller(0.5, 0.1, 0.6, 200, -150);  // random values. Do not use.
            Robo_Arm test = new Robo_Arm();
            Payload test2 = new Payload();
            //search payload below
            //generate search path for payload (list)
            int[] payloadWaypoint = new int[10];
            int base135 = 20; //first sweep arc
            int basen135 = -base135; //negative of first sweep arc
            short itranslated;

            for (int i = basen135; i < base135; i++)
            {
                itranslated = Robo_Arm.convertFromDegrees(i);
                test.Base.Move_To(itranslated);
                if (test2.is_payload)
                {
                    //center UGV based on turn angle
                }
                            
            }

            for (int i = 0; i < payloadWaypoint.Length; i++)
            {
                int nextWaypoint = payloadWaypoint[i];
                //move UGV to waypoint i here


                for (int j = basen135; j < base135; j++)
                {
                    itranslated = Robo_Arm.convertFromDegrees(j);
                    test.Base.Move_To(itranslated);
                    if (test2.is_payload)
                    {
                        Robo_Arm.centerUGV(j);
                        test.armStartPosition();

                        break;
                    }

                }
                if (test2.is_payload)
                {
                    break;
                }
            }

            do
            {
                //drive UGV to payload, within arm range
            } while (ugv_payload_threshold!=1);

            test.GrabPayload();
            test.CloseClaw();
            

        }

        public static short convertFromDegrees(int degree)
        {
            return (short)(degree / .088);
        }

        public static void centerUGV(int turretAngle)
        {
            //move ugv based on turret angle
        }

        public void armStartPosition()
        {
            Base.Move_To(Base_Start);
            Shoulder.Move_To(Shoulder_Start);
            Elbow.Move_To(Elbow_Start);
        }
           
    
        public void GrabPayload()
        {
            time = new TimeSpan(2, 0, 0);
            for (int i = Shoulder.dxl_present_position; i < Shoulder.DXL_MINIMUM_POSITION_VALUE; i++)
            {
                if()
                error_x = basePID.ControlVariable(time);
                error_y = shoulderPID.ControlVariable(time);
                Shoulder.Move_To((short)(Shoulder.dxl_present_position + error_y));
                Base.Move_To((short)(Base.dxl_present_position + error_x));
            }
           
        }
        public void CloseClaw()
        {
            //code for closing claw and retracting arm to UGV
        }
        
    }
}
