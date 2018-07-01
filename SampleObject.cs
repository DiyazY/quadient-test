using System;
using System.Collections.Generic;
using System.Text;

namespace quadient_test
{
    [Serializable]
    public class SampleObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> KeyValuePairs { get; set; }
        public IEnumerable<ListObject> Objects { get; set; }

        public static SampleObject InitObject(int id)
        {
            var obj = new SampleObject();
            obj.Id = id;
            obj.Name = $"obj-{id}";
            obj.KeyValuePairs = new Dictionary<string, string>()
            {
                {$"1-{id}-k",$"1-{id}-v"},
                {$"2-{id}-k",$"2-{id}-v"},
                {$"3-{id}-k",$"3-{id}-v"}
            };
            obj.Objects = new List<ListObject>()
            {
                new ListObject(){Id=Guid.NewGuid(),Name=$"lo-{id}"},
                new ListObject(){Id=Guid.NewGuid(),Name=$"lo-{id}"},
                new ListObject(){Id=Guid.NewGuid(),Name=$"lo-{id}"}
            };
            return obj;
        }
    }

    [Serializable]
    public class ListObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
