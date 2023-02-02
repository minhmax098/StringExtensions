using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace viettel.StringExtensions
{
    [Serializable]
    public class Coordinate
    {
        public float x; 
        public float y; 
        public float z;
        public static Coordinate InitCoordinate(Vector3 coordinate) 
        { 
            Coordinate coordinateConf = new Coordinate();
            coordinateConf.x = coordinate.x; 
            coordinateConf.y = coordinate.y;
            coordinateConf.z = coordinate.z; 
            return coordinateConf;
        }

        public static Coordinate InitCoordinateST(Vector3 coordinate)
        {
            Coordinate coordinateConf = new Coordinate();
            coordinateConf.x = coordinate.x;
            coordinateConf.y = coordinate.y;
            coordinateConf.z = coordinate.z;
            return coordinateConf;
        }
    }
}
