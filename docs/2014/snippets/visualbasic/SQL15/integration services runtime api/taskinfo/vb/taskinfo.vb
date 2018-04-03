' <snippetTaskInfoVB>
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.SqlServer.Dts.Runtime


Namespace TaskType
    _
   Class Program
      
      'Entry point which delegates to C-style main Private Function
      Public Overloads Shared Sub Main()
         Main(System.Environment.GetCommandLineArgs())
      End Sub
      
      Overloads Shared Sub Main(args() As String)
         Dim myApp As New Application()
         Dim tInfos As TaskInfos = myApp.TaskInfos
         
         ' Iterate through the collection, 
         ' printing values for the properties.
         Dim tInfo As TaskInfo
         For Each tInfo In  tInfos
            Console.WriteLine("CreationName:   {0}", tInfo.CreationName)
            Console.WriteLine("Description     {0}", tInfo.Description)
            Console.WriteLine("FileName        {0}", tInfo.FileName)
            ' Console.WriteLine("FileNameVersionString   {0}", tInfo.FileNameVersionString)
            Console.WriteLine("IconFile        {0}", tInfo.IconFile)
            Console.WriteLine("IconResource    {0}", tInfo.IconResource)
            Console.WriteLine("ID              {0}", tInfo.ID)
            Console.WriteLine("Name            {0}", tInfo.Name)
            ' Console.WriteLine("TaskContact     {0}", tInfo.TaskContact)
            Console.WriteLine("TaskType        {0}", tInfo.TaskType)
            Console.WriteLine("UITypeName      {0}", tInfo.UITypeName)
            Console.WriteLine("----------------------------")
         Next tInfo
         Console.WriteLine()
      End Sub 'Main
   End Class 'Program
End Namespace 'TaskType
' </snippetTaskInfoVB>