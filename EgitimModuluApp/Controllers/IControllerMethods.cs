using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EgitimModuluApp
{
    public interface IControllerMethods
    {
        ///<summary>
        ///Birden cok modeli maplemek icin kullanilacak method.
        ///<para>Ornek;</para>
        ///<para>mapMany&lt;<see cref="Entities.Student"/>,<see cref="EgitimModuluApp.StudentDto"/>&gt;(models); </para>
        ///</summary>
        List<Entity> mapMany<Entity, Model>(List<Model> models);
    }
}
