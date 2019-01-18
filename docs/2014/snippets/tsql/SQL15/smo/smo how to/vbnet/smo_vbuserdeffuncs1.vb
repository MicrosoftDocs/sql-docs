Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBUserDefFuncs1

    Sub Main()
        ' <snippetSMO_VBUserDefFuncs1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 2008R2 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Define a UserDefinedFunction object variable by supplying the parent database and the name arguments in the constructor.
        Dim udf As UserDefinedFunction
        udf = New UserDefinedFunction(db, "IsOWeek")
        'Set the TextMode property to false and then set the other properties.
        udf.TextMode = False
        udf.DataType = DataType.Int
        udf.ExecutionContext = ExecutionContext.Caller
        udf.FunctionType = UserDefinedFunctionType.Scalar
        udf.ImplementationType = ImplementationType.TransactSql
        'Add a parameter.
        Dim par As UserDefinedFunctionParameter
        par = New UserDefinedFunctionParameter(udf, "@DATE", DataType.DateTime)
        udf.Parameters.Add(par)
        'Set the TextBody property to define the user defined function.
        udf.TextBody = "BEGIN  DECLARE @ISOweek int SET @ISOweek= DATEPART(wk,@DATE)+1 -DATEPART(wk,CAST(DATEPART(yy,@DATE) as CHAR(4))+'0104') IF (@ISOweek=0) SET @ISOweek=dbo.ISOweek(CAST(DATEPART(yy,@DATE)-1 AS CHAR(4))+'12'+ CAST(24+DATEPART(DAY,@DATE) AS CHAR(2)))+1 IF ((DATEPART(mm,@DATE)=12) AND ((DATEPART(dd,@DATE)-DATEPART(dw,@DATE))>= 28)) SET @ISOweek=1 RETURN(@ISOweek) END;"
        'Create the user defined function on the instance of SQL Server.
        udf.Create()
        'Remove the user defined function.
        udf.Drop()
        ' </snippetSMO_VBUserDefFuncs1>
    End Sub

End Module
