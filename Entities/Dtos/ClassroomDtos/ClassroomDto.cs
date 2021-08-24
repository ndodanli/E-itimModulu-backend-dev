using System;
using System.Collections;
using System.Collections.Generic;
namespace Entities.Dtos.ClassroomDtos
{
    //veri tabanında nullable olarak belirtilen integer değerler
    //ve DateTime nesneleri nullable(?) olarak belirtildi.
    //required olarak belirtilen özelliklerin default değeri -1 verildi,
    //neden?
    //*nullable obje null olduğunda veri null olarak atanıyor,
    //eklemede ya da güncellemede sorun olmuyor(null olanlar güncellenmiyor, post edilirken de null geçiyor)

    //*required obje gönderilmediğinde -1 atanıyor, güncellenirken güncellenecek objeye geçmiyor,
    //post edilirken -1 olduğundan veri tabanı hatası fırlatıyor.

    public class ClassroomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SchoolId { get; set; } = -1;
        public string CreatedAtDate { get; set; }
        public string UpdatedAtDate { get; set; }
        public string UpdatedAtTime { get; set; }
        public string UpdatedBy { get; set; }
        public int[] TeacherIds { get; set; }
        private List<ClassroomDto> items = new List<ClassroomDto>();
        public IEnumerator GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

    }
}