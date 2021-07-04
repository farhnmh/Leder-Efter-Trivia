using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

class ClassSerializer
{
    public static byte[] ObjectToByteArray(object obj)
    {
        BinaryFormatter bf = new BinaryFormatter();
        bf.Binder = new CustomizedBinder();
        using (var ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
    }

    public static object ByteArrayToObject(byte[] arrBytes)
    {
        using (var memStream = new MemoryStream())
        {
            var binForm = new BinaryFormatter();
            binForm.Binder = new CustomizedBinder();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            var obj = binForm.Deserialize(memStream);
            return obj;
        }
    }
}

sealed class CustomizedBinder : SerializationBinder
{
    public override Type BindToType(string assemblyName, string typeName)
    {
        Type returntype = null;
        string sharedAssemblyName = "SharedAssembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
        assemblyName = Assembly.GetExecutingAssembly().FullName;
        typeName = typeName.Replace(sharedAssemblyName, assemblyName);
        returntype = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
        return returntype;
    }

    public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
    {
        base.BindToName(serializedType, out assemblyName, out typeName);
        assemblyName = "SharedAssembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
    }
}