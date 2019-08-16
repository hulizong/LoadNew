using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using VectorClass;
using WhatsNewAttributes;

namespace Attributes
{
    class Program
    {
        /// <summary>
        /// 输出的消息
        /// </summary>
        private static readonly StringBuilder outputText = new StringBuilder(1000);

        /// <summary>
        /// 存储的时间
        /// </summary>
        private static DateTime backDateTo = new DateTime(2017,2,1);
        static void Main(string[] args)
        { 
            //获取访问的程序集
            Assembly theAssembly = Assembly.Load(new AssemblyName("VectorClass"));
            //获取自定义特性的详细信息
            Attribute supportsAttribute = theAssembly.GetCustomAttribute(typeof(SupportsWhatsNewAttribute));
            AddToOutput($"assembly:{theAssembly.FullName}");
            if (supportsAttribute==null)
            {
                AddToOutput("这个程序集不支持");
                return;
            }
            else
            {
                AddToOutput("定义的类型是：");
            }
            //获取程序集中定义的公共类型集合
            IEnumerable<Type> types = theAssembly.ExportedTypes;
            foreach ( Type definedType in types)
            {
                DisplayTypeInfo(definedType);
            }
            Console.WriteLine(backDateTo);
            Console.WriteLine(outputText.ToString());
            Console.ReadLine();
        }


        public static void DisplayTypeInfo(Type type)
        {
            if (!type.GetTypeInfo().IsClass)
            {
                return;
            }
            AddToOutput($"{Environment.NewLine}类 {type.Name}");
            //获取类型的详细信息然后获取其自定义详细信息选择自定义特性再筛选时间
            IEnumerable<LastModifiedAttribute> lastModifiedAttributes = type.GetTypeInfo().GetCustomAttributes()
                .OfType<LastModifiedAttribute>().Where(a => a.DateModified >= backDateTo).ToArray();

            if (lastModifiedAttributes.Count()==0)
            {
                AddToOutput($"\t这个{type.Name}没有改变{Environment.NewLine}");
            }
            else
            {
                foreach (LastModifiedAttribute item in lastModifiedAttributes)
                {
                    WriteAttributeInfo(item);
                }
                AddToOutput("这些类的修改方法：");

                //获取类的信息中的方法
                foreach (MethodInfo methond in type.GetTypeInfo().DeclaredMembers.OfType<MethodInfo>())
                {
                    //获取这些方法的自定义特性信息筛选时间
                    IEnumerable<LastModifiedAttribute> attributesToMethods = methond.GetCustomAttributes().OfType<LastModifiedAttribute>()
                        .Where(a => a.DateModified >= backDateTo).ToArray();
                    if (attributesToMethods.Count()>0)
                    {
                        AddToOutput($"{methond.ReturnType}{methond.Name}()");
                        foreach (Attribute attribute in attributesToMethods)
                        {
                            WriteAttributeInfo(attribute);
                        }
                    }
                }
            }
        }


        static void AddToOutput(string Text) => outputText.Append("\n" + Text);

        private static void WriteAttributeInfo(Attribute attribute)
        {
            if (attribute is LastModifiedAttribute lastModifiedAttribute)
            {
                AddToOutput($"\tmodified:{lastModifiedAttribute.DateModified:D}:{lastModifiedAttribute.Changes}");
                if (lastModifiedAttribute.Issues!=null)
                {
                    AddToOutput($"\tOutstanding issues:{lastModifiedAttribute.Issues}");
                }
            }
        } 

    }
}
