---
title: "Using ADO to Execute SQLXML 4.0 Queries | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xml
ms.topic: "reference"
helpviewer_keywords: 
  - "query testers [SQLXML]"
  - "test scripts"
  - "ADO [SQLXML]"
  - "queries [SQLXML], ADO"
  - "SQLXML, ADO"
ms.assetid: 3d54e3bb-7c5f-427e-82f8-1403a54c4f53
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Using ADO to Execute SQLXML 4.0 Queries
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  In previous versions of SQLXML, HTTP-based query execution was supported using SQLXML IIS virtual directories and the SQLXML ISAPI filter. In SQLXML 4.0, these components have been removed as similar and overlapping functionality is provided with native XML Web services beginning in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
  
 As an alternative, you can execute queries and use SQLXML 4.0 with your COM-based applications, by leveraging the SQLXML extensions to ActiveX Data Objects (ADO) that were first introduced in Microsoft Data Access Components (MDAC) 2.6 and later.  
  
 This topic demonstrates using SQLXML and ADO as part of a Visual Basic Scripting Edition (VBScript) application (a script with the .vbs file name extension). It provides initial setup procedures to help you recreate and test query samples in the SQLXML 4.0 documentation.  
  
## Creating the SQLXML 4.0 Test Script  
 In this procedure, you create a VBScript (.vbs) file, Sqlxml4test.vbs, which can be used to execute SQLXML queries by leveraging the SQLXML ADO extensions in ADO 2.6 and later.  
  
#### To create the SQLXML 4.0 query tester using ADO (VBScript).  
  
1.  Copy the code below and paste it into a text file. Save the file as Sqlxml4test.vbs.  
  
    ```  
    WScript.Echo "Query process may take a few seconds to complete. Please be patient."  
  
    ' Note that for SQL Server Native Client to be used as the data provider,  
    ' it needs to be installed on the client computer first. Also, SQLXML extensions   
    ' for ADO are used and available in MDAC 2.6 or later.  
  
    'Set script variables.  
    inputFile = "@@FILE_NAME@@"  
    strServer = "@@SERVER_NAME@@"  
    strDatabase = "@@DATABASE_NAME@@"  
    dbGuid = "{5d531cb2-e6ed-11d2-b252-00c04f681b71}"  
  
    ' Establish ADO connection to SQL Server and   
    ' create an instance of the ADO Command object.  
    Set conn = CreateObject("ADODB.Connection")  
    Set cmd = CreateObject("ADODB.Command")  
    conn.Open "Provider=SQLXMLOLEDB.4.0;Data Provider=SQLNCLI11;Server=" & strServer & _  
              ";Database=" & strDatabase & ";Integrated Security=SSPI"  
    Set cmd.ActiveConnection = conn  
  
    ' Create the input stream as an instance of the ADO Stream object.  
    Set inStream = CreateObject("ADODB.Stream")  
    inStream.Open  
    inStream.Charset = "utf-8"  
    inStream.LoadFromFile inputFile  
  
    ' Set ADO Command instance to use input stream.  
    Set cmd.CommandStream = inStream  
  
    ' Set the command dialect.  
    cmd.Dialect = dbGuid  
  
    ' Set a second ADO Stream instance for use as a results stream.   
    Set outStream = CreateObject("ADODB.Stream")  
    outStream.Open  
  
    ' Set dynamic properties used by the SQLXML ADO command instance.   
    cmd.Properties("XML Root").Value = "ROOT"  
    cmd.Properties("Output Encoding").Value = "UTF-8"  
  
    ' Connect the results stream to the command instance and execute the command.  
    cmd.Properties("Output Stream").Value = outStream  
    cmd.Execute , , 1024  
  
    ' Echo cropped/partial results to console.  
    WScript.Echo Left(outStream.ReadText, 1023)  
  
    inStream.Close  
    outStream.Close  
    ```  
  
2.  Update the following script values for the sample you are trying to test and your test environment.  
  
    -   Find "`@@FILE_NAME@@`" and replace it with the name of your template file.  
  
    -   Find "`@@SERVER_NAME@@`" and replace it with the name of your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance (for example, "`(local)`" if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running locally).  
  
    -   Find "`@@DATABASE_NAME@@`" and replace it with the name of the database (for example, either "`AdventureWorks2012`" or "`tempdb`").  
  
     Update any other values if mentioned in the specific instructions for the example you are attempting to recreate locally on your computer.  
  
3.  Save the file and close it.  
  
4.  Verify that you have created any additional files, such as XML templates or schemas that are part of the sample you are attempting to recreate locally on your computer. These files should be located in the same directory where you have saved the test script file (Sqlxml4test.vbs).  
  
5.  Follow the instructions in the next section for how to use the SQLXML 4.0 test script.  
  
## Using the SQLXML 4.0 Test Script  
 The following procedure describes how to use the Sqlxml4test.vbs files to test the example queries provided in this documentation.  
  
#### To use the SQLXML 4.0 query tester  
  
1.  Verify that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client is installed, as follows:  
  
    1.  From the **Start** menu, point to **Settings**, and then click **Control Panel**.  
  
    2.  In Control Panel, open **Add or Remove Programs**  
  
    3.  In the list of currently installed programs, verify that **Microsoft SQL Server Native Client** appears in the list.  
  
        > [!NOTE]  
        >  If you need to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client, see [Installing SQL Server Native Client](../../relational-databases/native-client/applications/installing-sql-server-native-client.md).  
  
2.  Verify that the version of MDAC installed for the client computer is 2.6 or later. If you need to verify MDAC version information, you can use the MDAC Component Checker tool, which is provided as free download from the Microsoft Web site (www.microsoft.com). Fore more information, search on "MDAC Component Checker" on the Microsoft Web site.  
  
3.  Execute the script.  
  
     You can execute the VBScript file either at the command line using Cscript.exe or by double-clicking Sqlxml4test.vbs file to invoke the Windows Script Host (WScript.exe).  
  
     When executed, the script should display a message to alert you that the script might take a few moments to execute before returning and displaying query results as script output. When the output appears, compare its contents to the expected results for the sample.  
  
  
