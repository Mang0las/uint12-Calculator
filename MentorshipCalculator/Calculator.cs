using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorshipCalculator
{
    /// <summary>
    /// Class for calculator that stores numbers as 12 bit unsigned ints on a queue
    /// Commands:
    /// Put(value)
    /// Remove()
    /// Add()
    /// Sub()
    /// </summary>
    public class Calculator
    {
        //stores 12 bit unsigned integers, 2^12 min value = 0 max value = 4095
        private Queue<int> queue;


        public Calculator()
        {
            queue = new Queue<int>();
        }

        /// <summary>
        /// Puts a value on the memory queue, first converting it into a 12 bit unsigned int,
        /// except when the queue is full (5 values)
        /// </summary>
        /// <param name="value">Value to convert and enqueue</param>
        /// <returns>The converted value that was enqueued</returns>
        public int Put(int value)
        {
            if (queue.Count() == 5)
            {
                throw new InvalidOperationException($"The queue is full. {value} was not added.");
            }

            //Handles negative numbers, as our format only suppports positive ones ints
            //could change to math formula for O(1)
            while (value < 0)
            {
                value += 4096;
            }

            //Modulo operation to prevent overflowing and keep the value within the 12 bit range
            queue.Enqueue(value % 4096);
            return (value % 4096);
        }

        /// <summary>
        /// Removes the first value(dequeues) of the memory queue, if there is any value available
        /// </summary>
        public void Remove()
        {
            //exception unnecessary?
            if (queue.Count() == 0)
                return;

            queue.Dequeue();
        }

        /// <summary>
        /// Removes the first two values from the queue, adds them together, puts the result into the queue using Put(value),
        /// if there are fewer than 2 values on the queue, throws an exception
        /// </summary>
        /// <returns>The value added to the queue, after conversion, as returned by Put</returns>
        public int Add()
        {
            if (queue.Count() < 2)
                throw new InvalidOperationException("You must have at least 2 values on the queue to perform addition");

            int val1 = queue.Dequeue();
            int val2 = queue.Dequeue();

            return Put(val1 + val2);
        }

        /// <summary>
        /// Removes the first two values from the queue, subtracts the first value from the second one,
        /// puts the result into the queue using Put(value),
        /// if there are fewer than 2 values on the queue, throws an exception
        /// </summary>
        /// <returns>The value added to the queue, after conversion, as returned by Put</returns>
        public int Sub()
        {
            if (queue.Count() < 2)
                throw new InvalidOperationException("You must have at least 2 values on the queue to perform substraction");


            int val1 = queue.Dequeue();
            int val2 = queue.Dequeue();

            return Put(val2 - val1);
        }

        /// <summary>
        /// Helper method, returns a list of all the values in the memory queue for debugging, testing, or display purposes
        /// </summary>
        /// <returns>List<int> of the queue's values</returns>
        public List<int> GetQueueValues()
        {
            var values = new List<int>();
            foreach (var i in queue)
            {
                values.Add(i);
            }
            return values;
        }

        /// <summary>
        /// Helper method, returns the count of values in the memory queue for debugging, testing, or display purposes
        /// </summary>
        /// <returns>Count of items in queue</returns>
        public int Count()
        {
            return queue.Count();
        }

        /// <summary>
        /// Helper method, empties the queue for debugging, testing purposes
        /// </summary>
        public void Clear()
        {
            queue.Clear();
        }


    }
}
