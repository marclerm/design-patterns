using System;
using System.Collections.Generic;
using System.Text;

namespace BuilderDP
{
     class HtmlElement
     {
         public string Name, Text;
         public List<HtmlElement> Elements = new List<HtmlElement>();
         private const int indentSize = 2;

         public HtmlElement()
         {

         }

         public HtmlElement(string name, string text)
         {
             Name = name;
             Text = text;
         }

         private string ToStringImpl(int indent)
         {
             var sb = new StringBuilder();
             var i = new string(' ', indentSize * indent);
             sb.Append($"{i}<{Name}>\n");
             if (!string.IsNullOrWhiteSpace(Text))
             {
                 sb.Append(new string(' ', indentSize * (indent + 1)));
                 sb.Append(Text);
                 sb.Append("\n");
             }

             foreach (var e in Elements)
                 sb.Append(e.ToStringImpl(indent + 1));

             sb.Append($"{i}</{Name}>\n");
             return sb.ToString();
         }

         public override string ToString()
         {
             return ToStringImpl(0);
         }
     }

     class HtmlBuilder
     {
         private readonly string rootName;

         public HtmlBuilder(string rootName)
         {
             this.rootName = rootName;
             root.Name = rootName;
         }

         // not fluent
         public void AddChild(string childName, string childText)
         {
             var e = new HtmlElement(childName, childText);
             root.Elements.Add(e);
         }

         public HtmlBuilder AddChildFluent(string childName, string childText)
         {
             var e = new HtmlElement(childName, childText);
             root.Elements.Add(e);
             return this;
         }

         public override string ToString()
         {
             return root.ToString();
         }

         public void Clear()
         {
             root = new HtmlElement { Name = rootName };
         }

         HtmlElement root = new HtmlElement();
     }

     public class Demo
     {
         static void Main(string[] args)
         {
             // if you want to build a simple HTML paragraph using StringBuilder
             var hello = "hello";
             var sb = new StringBuilder();
             sb.Append("<p>");
             sb.Append(hello);
             sb.Append("</p>");
             Console.WriteLine(sb);

             // now I want an HTML list with 2 words in it
             var words = new[] { "hello", "world" };
             sb.Clear();
             sb.Append("<ul>");
             foreach (var word in words)
             {
                 sb.AppendFormat("<li>{0}</li>", word);
             }
             sb.Append("</ul>");
             Console.WriteLine(sb);

             // ordinary non-fluent builder
             var builder = new HtmlBuilder("ul");
             builder.AddChild("li", "hello");
             builder.AddChild("li", "world");
             Console.WriteLine(builder.ToString());

             // fluent builder
             sb.Clear();
             builder.Clear(); // disengage builder from the object it's building, then...
             builder.AddChildFluent("li", "hello").AddChildFluent("li", "world");
             Console.WriteLine(builder);
         }
     }

  //Exercise
  /*

    namespace Coding.Exercise
    {
        public class FieldElement
        {
            public string FieldName, FieldType;
            public List<FieldElement> Elements = new List<FieldElement>();
            public const int spaceIndent = 2;
            public bool IsRoot;

            public FieldElement()
            {

            }
            public FieldElement(string name, string valuetype)
            {
                FieldName = name;
                FieldType = valuetype;
            }

            private string ToStringImpl(int indent)
            {
                var sb = new StringBuilder();
                var i = new string(' ', spaceIndent * indent);
                
               
                if (!string.IsNullOrWhiteSpace(FieldName))
                {                   
                    sb.Append("public ");
                    sb.Append($"{FieldType} ");
                    sb.Append($"{FieldName}");
                    if (IsRoot)
                        sb.Append("\n");
                    else
                        sb.Append(";\n");
                }

                if (IsRoot)
                    sb.Append("{\n");
                foreach (var e in Elements)
                {
                    sb.Append(new string(' ', spaceIndent * (indent + 1)));
                    sb.Append(e.ToStringImpl(indent + 1));
                }
                if (IsRoot)
                    sb.Append("}");
                return sb.ToString();
            }

            public override string ToString()
            {
                return ToStringImpl(0);
            }
        }
        public class CodeBuilder
        {
            // TODO
            private readonly string rootName;
            FieldElement root = new FieldElement();

            public CodeBuilder(string rootName)
            {
                this.rootName = rootName;
                root.FieldName = rootName;
                root.FieldType = "class";
                root.IsRoot = true;
            }

            public CodeBuilder AddField(string fuieldName, string fieldType)
            {
                var e = new FieldElement(fuieldName, fieldType);
                root.Elements.Add(e);
                return this;
            }

            public override string ToString()
            {
                return root.ToString();
            }

           
        }

        public class Exercise
        {
            static void Main(string[] args)
            {
                var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
                Console.WriteLine(cb);
            }
        }
    }*/

}