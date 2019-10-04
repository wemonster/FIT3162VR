using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace A
{
    public class ArcData
    {
        public Vector3 startpos;
        public Vector3 endpos;
        public float volume;
        public string OriginState;
        public string DestState;
        public string OSLIC;
        public string DSLIC;

        public ArcData(Vector3 startpos,Vector3 endpos,float volume,string OriginState,string DestState, string OSLIC, string DSLIC)
        {
            this.startpos = startpos;
            this.endpos = endpos;
            this.volume = volume;
            this.OriginState = OriginState;
            this.DestState = DestState;
            this.OSLIC = OSLIC;
            this.DSLIC = DSLIC;
        }
    }
}
