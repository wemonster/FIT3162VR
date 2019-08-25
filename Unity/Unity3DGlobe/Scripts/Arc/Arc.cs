using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace A
{
    public class Arc
    {
        public Vector3 startpos;
        public Vector3 endpos;
        public float volume;
        public string OriginState;
        public string DestState;

        public Arc(Vector3 startpos,Vector3 endpos,float volume,string OriginState,string DestState)
        {
            this.startpos = startpos;
            this.endpos = endpos;
            this.volume = volume;
            this.OriginState = OriginState;
            this.DestState = DestState;

        }
    }
}
