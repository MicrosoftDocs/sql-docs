---
title: "Passing Parameters to Updategrams (SQLXML 4.0) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "nullvalue attribute"
  - "passing parameters [SQLXML]"
  - "parameters [SQLXML]"
  - "updategrams [SQLXML], passing parameters"
  - "null values [SQLXML]"
ms.assetid: 2354e6e7-1860-471f-8711-4e374c5a4ed2
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Passing Parameters to Updategrams (SQLXML 4.0)
  Updategrams are templates; therefore, you can pass them parameters. For more information about passing parameters to templates, see [Updategram Security Considerations &#40;SQLXML 4.0&#41;](../security/updategram-security-considerations-sqlxml-4-0.md).  
  
 Updategrams allow you to pass NULL as a parameter value. To pass the NULL parameter value, you specify the `nullvalue` attribute. The value that is assigned to the `nullvalue` attribute is then provided as the parameter value. Updategrams treat this value as NULL.  
  
> [!NOTE]  
>  In `<sql:header>` and `<updg:header>`, you should specify the `nullvalue` as unqualified; whereas, in `<updg:sync>`, you specify the `nullvalue` as qualified (for example, `updg:nullvalue`).  
  
## Examples  
 To create working samples using the following examples, you must meet the requirements specified in [Requirements for Running SQLXML Examples](../../sqlxml/requirements-for-running-sqlxml-examples.md).  
  
 Before using the updategram examples, note the following:  
  
-   The examples use default mapping (that is, no mapping schema is specified in the updategram). For more examples of updategrams that use mapping schemas, see [Specifying an Annotated Mapping Schema in an Updategram &#40;SQLXML 4.0&#41;](specifying-an-annotated-mapping-schema-in-an-updategram-sqlxml-4-0.md).  
  
### A. Passing parameters to an updategram  
 In this example, the updategram changes the last name of an employee in the HumanResources.Shift table. The updategram is passed two parameters: ShiftID, which is used to uniquely identify a shift, and Name.  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
<updg:header>  
  <updg:param name="ShiftID"/>  
  <updg:param name="Name" />  
</updg:header>  
  <updg:sync >  
    <updg:before>  
       <HumanResources.Shift ShiftID="$ShiftID" />  
    </updg:before>  
    <updg:after>  
      <HumanResources.Shift Name="$Name" />  
    </updg:after>  
  </updg:sync>  
</ROOT>  
```  
  
##### To test the updategram  
  
1.  Copy the updategram above into Notepad and save it to file as UpdategramWithParameters.xml.  
  
2.  Prepare the SQLXML 4.0 test script (Sqlxml4test.vbs) in [Using ADO to Execute SQLXML 4.0 Queries](../../sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md) to execute the updategram by adding the following lines after the `cmd.Properties("Output Stream").Value = outStream`:  
  
    ```  
    cmd.NamedParameters = True  
    ' CreateParameter arguments: Name, Type, Direction, Size, Value  
    cmd.Parameters.Append cmd.CreateParameter("@ShiftID",  2, 1,  0, 1)  
    cmd.Parameters.Append cmd.CreateParameter("@Name",   200, 1, 50, "New Name")  
    ```  
  
### B. Passing NULL as a parameter value to an updategram  
 In executing an updategram, the "isnull" value is assigned to the parameter that you want to set to NULL. Updategram converts the "isnulll" parameter value to NULL and processes it accordingly.  
  
 The following updategram sets an employee title to NULL:  
  
```  
<ROOT xmlns:updg="urn:schemas-microsoft-com:xml-updategram">  
<updg:header nullvalue="isnull" >  
  <updg:param name="EmployeeID"/>  
  <updg:param name="ManagerID" />  
</updg:header>  
  <updg:sync >  
    <updg:before>  
       <HumanResources.Employee EmployeeID="$EmployeeID" />  
    </updg:before>  
    <updg:after>  
      <HumanResources.Employee ManagerID="$ManagerID" />  
    </updg:after>  
  </updg:sync>  
</ROOT>  
```  
  
##### To test the updategram  
  
1.  Copy the updategram above into Notepad and save it to file as UpdategramPassingNullvalues.xml.  
  
2.  Prepare the SQLXML 4.0 test script (Sqlxml4test.vbs) in [Using ADO to Execute SQLXML 4.0 Queries](../../sqlxml/using-ado-to-execute-sqlxml-4-0-queries.md) to execute the updategram by adding the following lines after the `cmd.Properties("Output Stream").Value = outStream`:  
  
    ```  
    cmd.NamedParameters = True  
    ' CreateParameter arguments: Name, Type, Direction, Size, Value   
    cmd.Parameters.Append cmd.CreateParameter("@EmployeeID", 3, 1, 0, 1)  
    cmd.Parameters.Append cmd.CreateParameter("@ManagerID",  3, 1, 0, Null)  
    ```  
  
## See Also  
 [Updategram Security Considerations &#40;SQLXML 4.0&#41;](../security/updategram-security-considerations-sqlxml-4-0.md)  
  
  
