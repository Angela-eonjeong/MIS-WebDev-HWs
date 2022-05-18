using Kim_Angela_Eonjeong_HW5.DAL;
using System;
using System.Linq;

namespace Kim_Angela_Eonjeong_HW5.Utilities
{
    public static class GenerateProductID
    {
        public static Int32 GetNextProductID(AppDbContext _context)
        {
            //Set a number where the Product numbers should start
            const Int32 START_NUMBER = 3000;

            Int32 intMaxProductID; //the current maximum Product number
            Int32 intNextProductID; //the Product number for the next class

            if (_context.Products.Count() == 0) //there are no Products in the database yet
            {
                intMaxProductID = START_NUMBER; //Product numbers start at 3001
            }
            else
            {
                intMaxProductID = _context.Products.Max(c => c.ProductID); //this is the highest number in the database right now
            }

            //You added Products before you realized that you needed this code
            //and now you have some Product numbers less than 3000
            if (intMaxProductID < START_NUMBER)
            {
                intMaxProductID = START_NUMBER;
            }

            //add one to the current max to find the next one
            intNextProductID = intMaxProductID + 1;

            //return the value
            return intNextProductID;
        }

    }
}