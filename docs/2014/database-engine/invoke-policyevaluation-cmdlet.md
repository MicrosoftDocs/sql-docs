---
title: "Invoke-PolicyEvaluation cmdlet | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "Policy-Based Management, Invoke-PolicyEvaluation"
  - "Policy-Based Management, PowerShell"
  - "Invoke-PolicyEvaluation cmdlet"
  - "Cmdlets [SQL Server], Invoke-PolicyEvaluation"
  - "PowerShell [SQL Server], Invoke-PolicyEvaluation"
ms.assetid: 3e6d4f5a-59b7-4203-b95a-f7e692c0f131
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Invoke-PolicyEvaluation cmdlet
  **Invoke-PolicyEvaluation** is a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] cmdlet that reports whether a target set of SQL Server objects complies with the conditions specified in one or more Policy-Based Management policies.  
  
## Using Invoke-PolicyEvaluation  
 **Invoke-PolicyEvaluation** evaluates one or more policies against a set of SQL Server objects called the target set. The set of target objects comes from a target server. Each policy defines conditions, which are the allowed states for the target objects. For example, the **Trustworthy Database** policy states that the TRUSTWORTHY database property must be set to OFF.  
  
 The **-AdHocPolicyEvaluationMode** parameter specifies the actions taken:  
  
 Check  
 Report the compliance status of the target objects using the credentials of your current login. Do no reconfigure any objects. This is the default setting.  
  
 CheckSqlScriptAsProxy  
 Report the compliance status of the target objects using the credentials of the **##MS_PolicyTSQLExecutionLogin##** proxy login. Do no reconfigure any objects.  
  
 Configure  
 Report the compliance status of the target objects using the credentials of your current login. Reconfigure any settable and deterministic options that are not in compliance with the policies.  
  
## Specifying Polices  
 How you specify a policy depends on where the policy is stored. Policies can be stored in two formats:  
  
-   They can be objects stored in a policy store, such as an instance of the Database Engine. You can use the SQLSERVER:\SQLPolicy folder to specify the location of policies in a policy store. You can use Windows PowerShell cmdlets to filter the input polices based on their properties, such as using Where-Object to filter on the policy category or Get-Item to filter on policy name.  
  
-   They can be exported as XML files. You can use a file system drive, such as D:, to specify the location of the XML files. You can use Windows PowerShell cmdlets such as Where-Object to filter the policies on their file properties, such as file name.  
  
 If the policies are stored in a policy store, you must pass in a set of PSObjects pointing to the policies to be evaluated. This is typically done by piping the output of a cmdlet such as Get-Item to **Invoke-PolicyEvaluation**, and does not require that you specify the **-Policy** parameter. For example, if you have imported the Microsoft Best Practices policies into your instance of the database engine, this command evaluates the **Database Status** policy:  
  
```  
sl "SQLSERVER:\SQLPolicy\MyComputer\DEFAULT\Policies"  
Get-Item "Database Status" | Invoke-PolicyEvaluation -TargetServerName "MYCOMPUTER"  
```  
  
 This example shows using Where-Object to filter multiple policies from a policy store based on their **PolicyCategory** property. The objects from the piped output of **Where-Object** is consumed by **Invoke-PolicyEvaluation**.  
  
```  
sl "SQLSERVER:\SQLPolicy\MyComputer\DEFAULT\Policies"  
gci | Where-Object {$_.PolicyCategory -eq "Microsoft Best Practices: Maintenance"} | Invoke-PolicyEvaluation -TargetServer "MYCOMPUTER"  
```  
  
 If the policies are stored as XML files, you must use the **-Policy** parameter to supply both the path and name for each policy. If you do not specify a path in the **-Policy** parameter, **Invoke-PolicyEvaulation** uses the current setting of the **sqlps** path. For example, this command evaluates one of the Microsoft Best Practice policies installed with SQL Server against the default database for your login:  
  
```  
Invoke-PolicyEvaluation -Policy "C:\Program Files\Microsoft SQL Server\120\Tools\Policies\DatabaseEngine\1033\Database Status.xml" -TargetServerName "MYCOMPUTER"  
```  
  
 This command does the same thing, only it uses the current **sqlps** path to establish the location of the policy XML file:  
  
```  
sl "C:\Program Files\Microsoft SQL Server\120\Tools\Policies\DatabaseEngine\1033"  
Invoke-PolicyEvaluation -Policy "Database Status.xml" -TargetServerName "MYCOMPUTER"  
```  
  
 This example shows using the **Get-ChildItem** cmdlet to retrieve multiple policy XML files and pipe the objects into **Invoke-PolicyEvaluation**:  
  
```  
sl "C:\Program Files\Microsoft SQL Server\120\Tools\Policies\DatabaseEngine\1033"  
gci "Database Status.xml", "Trustworthy Database.xml" | Invoke-PolicyEvaluation -TargetServer "MYCOMPUTER"  
```  
  
## Specifying the Target Set  
 Use three parameters to specify the set of target objects:  
  
-   **-TargetServerName** specifies the instance of SQL Server containing the target objects. You can specify the information in a string that uses the format defined for the ConnectionString property of the <xref:System.Data.SqlClient.SqlConnection> class. You can use the <xref:System.Data.SqlClient.SqlConnectionStringBuilder> class to build a correctly formatted connection string. You can also create a <xref:Microsoft.SqlServer.Management.Sdk.Sfc.SqlStoreConnection> object and pass it to **-TargetServer**. If you supply a string that has only the name of the server, **Invoke-PolicyEvaluation** uses Windows Authentication to connect to the server.  
  
-   **-TargetObjects** takes an object or array of objects that represent the SQL Server objects in the target set. For example, you could create an array of <xref:Microsoft.SqlServer.Management.Smo.Database> class objects to pass in to **-TargetObjects**.  
  
-   **-TargetExpressions** takes a string containing a query expression that specifies the objects in the target set. The query expression is in the form of nodes separated by the '/' character. Each node is in the form ObjectType[Filter]. Object type is one of the objects in a SQL Server Management Object (SMO) object hieararchy. Filter is an expression that filters for objects at that node. For more information, see [Query Expressions and Uniform Resource Names](../powershell/query-expressions-and-uniform-resource-names.md).  
  
 Specify either **-TargetObjects** or **-TargetExpression**, not both.  
  
 This example uses an Sfc.SqlStoreConnection object to specify the target server:  
  
```  
sl "C:\Program Files\Microsoft SQL Server\120\Tools\Policies\DatabaseEngine\1033"  
$conn = New-Object Microsoft.SqlServer.Management.Sdk.Sfc.SqlStoreConnection("server='MYCOMPUTER';Trusted_Connection=True")  
Invoke-PolicyEvaluation -Policy "Database Status.xml" -TargetServerName $conn  
```  
  
 This example uses **-TargetExpression** to identify the specific database to evaluate:  
  
```  
sl "C:\Program Files\Microsoft SQL Server\120\Tools\Policies\DatabaseEngine\1033"  
Invoke-PolicyEvaluation -Policy "Database Status.xml" -TargetServerName "MyComputer" -TargetExpression "Server[@Name='MYCOMPUTER']/Database[@Name='AdventureWorks2012']"  
```  
  
## Evaluating Analysis Services Policies  
 To evaluate policies against an instance of [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], you must load and register an assembly into PowerShell, create a variable with an Analysis Services connection object, and pass the variable to the **-TargetObject** parameter. This example shows evaluating the Best Practices surface area configuration policy for [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]:  
  
```  
sl "C:\Program Files\Microsoft SQL Server\120\Tools\Policies\AnalysisServices\1033"  
[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.AnalysisServices")  
$SSASsvr = new-object Microsoft.AnalysisServices.Server  
$SSASsvr.Connect("Data Source=Localhost")  
Invoke-PolicyEvaluation -Policy "Surface Area Configuration for Analysis Services Features.xml" -TargetObject $SSASsvr  
```  
  
## Evaluating Reporting Services Policies  
 To evaluate [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] policies, you must load and register an assembly into PowerShell, create a variable with a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] connection object, and pass the variable to the **-TargetObject** parameter. This example shows evaluating the Best Practices surface area configuration policy for [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]:  
  
```  
sl "C:\Program Files\Microsoft SQL Server\120\Tools\Policies\ReportingServices\1033"  
[System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.Dmf.Adapters")  
$SSRSsvr = new-object Microsoft.SqlServer.Management.Adapters.RSContainer('MyComputer')  
Invoke-PolicyEvaluation -Policy "Surface Area Configuration for Reporting Services 2008 Features.xml" -TargetObject $SSRSsvr  
```  
  
## Formatting Output  
 By default, the output of **Invoke-PolicyEvaluation** is displayed in the command prompt window as a concise report in human-readable format. You can use the **-OutputXML** parameter to specify that the cmdlet instead produce a detailed report as an XML file. **Invoke-PolicyEvaluation** uses the Systems Modeling Language Interchange Format (SML-IF) schema so the file can be read by SML-IF readers.  
  
```  
sl "SQLSERVER:\SQLPolicy\MyComputer\DEFAULT\Policies"  
Invoke-PolicyEvaluation -Policy "Datbase Status" -TargetServer "MYCOMPUTER" -OutputXML > C:\MyReports\DatabaseStatusReport.xml  
```  
  
## See Also  
 [Use the Database Engine cmdlets](../../2014/database-engine/use-the-database-engine-cmdlets.md)  
  
  
