using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliminateGame
{
    public class RegionAggregationHelper
    {
        /// <summary>
        /// 聚合相同类型栅格
        /// </summary>
        /// <param name="map">星星:1，三角：2，方块：3</param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static List<List<int>> GatherGrids(int[] map,int height,int width)
        {
            var l = map.Length;
            var color = new bool[l];
            var result=new List<List<int>>();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (color[i*width + j])
                    {
                        continue;
                    }

                    var region = new List<int>();
                    Stack<int> stack=new Stack<int>();
                    stack.Push(i*width + j);
                    color[i*width + j] = true;

                    while (stack.Count>0)
                    {
                        var index = stack.Pop();
                        region.Add(index);
                        color[index] = true;
                        var style = map[index];

                        if (index%height != 0)
                        {
                            GatherNextStep(map, index - 1, l, color, style, stack);
                        }

                        if (index%height != width - 1)
                        {
                            GatherNextStep(map, index + 1, l, color, style, stack);
                        }
                        GatherNextStep(map, index-width, l, color, style, stack);
                        GatherNextStep(map, index+width, l, color, style, stack);
                    }

                    result.Add(region);
                }
            }


            return result;
        }

        private static void GatherNextStep(int[] map, int nextIndex, int l, bool[] color, int style, Stack<int> stack)
        {
            if (IsIndexValid(nextIndex, l) && !color[nextIndex] && map[nextIndex] == style)
            {
                stack.Push(nextIndex);
            }
        }

        private static bool IsIndexValid(int index, int l)
        {
            return index >= 0 && index < l;
        }
    }
}
