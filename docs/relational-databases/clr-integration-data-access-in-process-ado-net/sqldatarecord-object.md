---
title: "SqlDataRecord Object"
description: In SQL Server CLR integration, stored procedures can use the SqlDataRecord class to send custom result sets to the client.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "SqlDataRecord object"
  - "custom result sets [CLR integration]"
ms.assetid: 2ed667fb-749c-4280-a8fb-650643683c8f
---
# SqlDataRecord Object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **SqlDataRecord** object represents a single row of data, along with its related metadata.  
  
 Managed stored procedures may send to the client result sets that are not from a **SqlDataReader**. The **SqlDataRecord** class, along with **SendResultsStart**, **SendResultsRow**, and **SendResultsEnd** methods of the **SqlPipe** object, allows stored procedures to send custom result sets to the client.  
  
 For more information, see the **Microsoft.SqlServer.Server.SqlDataRecord** class reference documentation in the .NET Framework SDK documentation.  
  
## Example  
 The following example creates a new employee record and returns it to the caller.  
  
 C#  
  
```  
[Microsoft.SqlServer.Server.SqlProcedure]  
public static void CreateNewRecordProc()  
{  
    // Variables.         
    SqlDataRecord record;  
  
    // Create a new record with the column metadata.  The constructor   
    // is able to accept a variable number of parameters.  
    record = new SqlDataRecord(new SqlMetaData("EmployeeID", SqlDbType.Int),  
                               new SqlMetaData("Surname", SqlDbType.NVarChar, 20),  
                               new SqlMetaData("GivenName", SqlDbType.NVarChar, 20),  
                               new SqlMetaData("StartDate", SqlDbType.DateTime) );  
  
    // Set the record fields.  
    record.SetInt32(0, 0042);  
    record.SetString(1, "Funk");  
    record.SetString(2, "Don");  
    record.SetDateTime(3, new DateTime(2005, 7, 17));  
  
    // Send the record to the calling program.  
    SqlContext.Pipe.Send(record);  
  
}  
```  
  
 Visual Basic  
  
```  
<Microsoft.SqlServer.Server.SqlProcedure()> _  
Public Shared Sub  CreateNewRecordVBProc ()  
    ' Variables.  
    Dim record As SqlDataRecord  
  
    ' Create a new record with the column metadata. The constructor is   
    ' able to accept a variable number of parameters  
  
    record = New SqlDataRecord(New SqlMetaData("EmployeeID", SqlDbType.Int), _  
                           New SqlMetaData("Surname", SqlDbType.NVarChar, 20), _  
                           New SqlMetaData("GivenName", SqlDbType.NVarChar, 20), _  
                           New SqlMetaData("StartDate", SqlDbType.DateTime))  
  
    ' Set the record fields.  
    record.SetInt32(0, 42)  
    record.SetString(1, "Funk")  
    record.SetString(2, "Don")  
    record.SetDateTime(3, New DateTime(2005, 7, 17))  
  
    ' Send the record to the calling program.  
    SqlContext.Pipe.Send(record)  
  
End Sub  
```  
  
## See Also  
 [SqlPipe Object](../../relational-databases/clr-integration-data-access-in-process-ado-net/sqlpipe-object.md)  
  
  
