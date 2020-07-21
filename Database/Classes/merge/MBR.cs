using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISO_Classes;
namespace FAD3.Database.Classes.merge
{
    public class MBR
    {
        public bool IsValid { get; set; }

        public Grid25GridCell UpperLeftGrid { get; private set; }
        public Grid25GridCell LowerRightGrid { get; private set; }

        public Coordinate UpperLeftCoordinate { get; set; }
        public Coordinate LowerRightCoordinate { get; set; }
        public MBR(Grid25GridCell upperLeft, Grid25GridCell lowerRight)
        {
            if (upperLeft.IsValid && lowerRight.IsValid)
            {
                int upperLeftX;
                int upperLeftY;
                int lowerRightX;
                int lowerRightY;

                Grid25GridCell.Grid25CellToUTMString(upperLeft, out upperLeftX, out upperLeftY);
                Grid25GridCell.Grid25CellToUTMString(lowerRight, out lowerRightX, out lowerRightY);
                IsValid = upperLeftX < lowerRightX && lowerRightY < upperLeftY;
                if (IsValid)
                {
                    UpperLeftGrid = upperLeft;
                    LowerRightGrid = lowerRight;
                    UpperLeftCoordinate = Grid25GridCell.Grid25CellToLongLatCoordinate(upperLeft);
                    LowerRightCoordinate = Grid25GridCell.Grid25CellToLongLatCoordinate(lowerRight);
                }
                else
                {
                    throw new Exception("Grid corners location is incorrect.");
                }
            }
            else
            {
                throw new Exception("One or both grid cell corners are not valid");
            }


        }

        public MBR(Coordinate upperLeft, Coordinate lowerRight)
        {
            float upperLeftX = UpperLeftCoordinate.Longitude;
            float upperLeftY = UpperLeftCoordinate.Latitude;
            float lowerRightX =lowerRight.Longitude;
            float lowerRightY=lowerRight.Latitude;

            IsValid = upperLeftX < lowerRightX && lowerRightY < upperLeftY;
        }

    }
}
