using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboArm_UGV
{
    class Payload
    {
        #region Properties
        public bool is_payload { get; set; }
        public bool is_color1 { get; set; }
        public bool is_color2 { get; set; }
        public bool is_color3 { get; set; }
        public bool is_rectangle { get;set;}
        public double dist_appr { get; set; }
        public int x_coor { get; set; }
        public int y_coor { get; set; }

        #endregion Properties

        public Payload()
        {
            is_payload = false;
            is_color1 = false;
            is_color2 = false;
            is_color3 = false;
            is_rectangle = false;
            dist_appr = 50;
            x_coor = 0;
            y_coor = 0;
        }

    }


}
