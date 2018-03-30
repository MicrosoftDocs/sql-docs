' <snippetApplication_DataTypeInfosVB>
class ApplicationTests1
    Sub Main(ByVal args() As String)
        Dim dti As DataTypeInfos =  New Application().DataTypeInfos 
 
        For Each x As DataTypeInfo In dti
            Console.WriteLine(x.TypeName + " , " + x.TypeEnumName)
        Next
    End Sub
End Class
' </snippetApplication_DataTypeInfosVB>
' <snippetApplication_DBProviderInfosVB>
class ApplicationTests
    Sub Main(ByVal args() As String)
        Dim app As Application =  New Application() 
        Dim dbis As DBProviderInfos =  app.DBProviderInfos 
        Console.WriteLine("Database Provider Info:")
        For Each dbi As DBProviderInfo In dbis
            Console.WriteLine(dbi.Description)
        Next
    End Sub
End Class
' </snippetApplication_DBProviderInfosVB>
' <snippetApplication_ForEachEnumeratorInfosVB>
class ApplicationTests
    Sub Main(ByVal args() As String)
        Dim app As Application =  New Application() 
        Dim fee As ForEachEnumeratorInfos =  app.ForEachEnumeratorInfos 
        For Each x As ForEachEnumeratorInfo In fee
            Console.WriteLine(x.ID)
        Next
    End Sub
End Class
' </snippetApplication_ForEachEnumeratorInfosVB>
' <snippetApplication_LogProviderInfosVB>
class ApplicationTests
    Sub Main(ByVal args() As String)
        Dim app As Application =  New Application() 
        Dim lpis As LogProviderInfos =  app.LogProviderInfos 
        For Each x As LogProviderInfo In lpis
            Console.WriteLine(x.Name)
        Next
    End Sub
End Class
' </snippetApplication_LogProviderInfosVB>
' <snippetApplication_PipelineComponentInfosVB>
class ApplicationTests
    Sub Main(ByVal args() As String)
        Dim app As Application =  New Application() 
        Dim pcis As PipelineComponentInfos =  app.PipelineComponentInfos 
        For Each x As PipelineComponentInfo In pcis
            Console.WriteLine(x.CreationName)
        Next
    End Sub
End Class
' </snippetApplication_PipelineComponentInfosVB>
' <snippetApplication_TaskInfosVB>
class ApplicationTests
    Sub Main(ByVal args() As String)
        Dim app As Application =  New Application() 
        Dim tis As TaskInfos =  app.TaskInfos 
        For Each x As TaskInfo In tis
            Console.WriteLine(x.FileName + "..." + x.Description)
        Next
    End Sub
End Class
' </snippetApplication_TaskInfosVB>




