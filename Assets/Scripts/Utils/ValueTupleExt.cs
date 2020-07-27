using System;

namespace ThisGame.Utils {
    public static class ValueTupleExt {
        public static ref T At<T>(this ref (T, T, T) tuple, int idx) {
            switch(idx) {
                case 0:
                    return ref tuple.Item1;
                case 1:
                    return ref tuple.Item2;
                case 2:
                    return ref tuple.Item3;
                default:
                    throw new IndexOutOfRangeException("ValueTupleExt.At() out of range");
            }
        }


        public static bool Any(this ref (uint, uint, uint) tuple, uint val, out int idx) {
            if(tuple.Item1 == val) {
                idx = 0;
                return true;
            }
            if(tuple.Item2 == val) {
                idx = 1;
                return true;
            }
            if(tuple.Item3 == val) {
                idx = 2;
                return true;
            }
            idx = -1;
            return false;
        }
    }
}
