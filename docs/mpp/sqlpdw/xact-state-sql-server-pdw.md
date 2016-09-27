---
title: "XACT_STATE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 09ddcbf5-cd0b-4452-b01d-1ccab388b480
caps.latest.revision: 10
author: BarbKess
---
# XACT_STATE (SQL Server PDW)
Is a scalar function that reports the user transaction state of a current running request. XACT_STATE indicates whether the request has an active user transaction, and whether the transaction is capable of being committed.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
XACT_STATE()  
```  
  
## Return Type  
**smallint**  
  
## Remarks  
XACT_STATE returns the following values.  
  
|Return value|Meaning|  
|----------------|-----------|  
|1|The current request has an active user transaction. The request can perform any actions, including writing data and committing the transaction.|  
|0|There is no active user transaction for the current request.|  
|-1|This state exists in SQL Server, but is not exposed in PDW. The definition below is for comparison to SQL Server behavior.<br /><br />The current request has an active user transaction, but an error has occurred that has caused the transaction to be classified as an uncommittable transaction. The request cannot commit the transaction or roll back to a savepoint; it can only request a full rollback of the transaction. The request cannot perform any write operations until it rolls back the transaction. The request can only perform read operations until it rolls back the transaction. After the transaction has been rolled back, the request can perform both read and write operations and can begin a new transaction.<br /><br />When a batch finishes running, the Database Engine will automatically roll back any active uncommittable transactions. If no error message was sent when the transaction entered an uncommittable state, when the batch finishes, an error message will be sent to the client application. This message indicates that an uncommittable transaction was detected and rolled back.|  
|-2|The current request has an active user transaction, but an error has occurred that caused the transaction to be classified as a **rollback-only** transaction.<br /><br />When transaction reaches this state, you can execute the following statements: **SET**, **DECLARE**, **PRINT**, **RAISERROR**, **THROW**, and control-flow statements (**IF**/**ELSE**, **WHILE**, **TRY**…**CATCH**).<br /><br />Transactions in this state are automatically rolled-back at the end of the batch, unless an explicit **ROLLBACK** is requested earlier.|  
  
Both the XACT_STATE and @@TRANCOUNT functions can be used to detect whether the current request has an active user transaction. @@TRANCOUNT cannot be used to determine whether that transaction has been classified as an uncommittable transaction. XACT_STATE cannot be used to determine whether there are nested transactions.  
  
Example of an error handling flow and progression of XACT_STATE:  
  
|Step|Statement|XACT_STATE|Comments|  
|--------|-------------|---------------|------------|  
|1|BEGIN TRAN|0|Beginning transaction|  
|2|BEGIN TRY|1|Transaction is active.|  
|3|SELECT 1/0|1|Division by zero happens here.|  
||...|N/A|Control passed to CATCH block.|  
||END TRY|N/A||  
|4|BEGIN CATCH|-2|Transaction is now in a rollback-only state|  
|5|DECLARE @err int = @@ERROR|-2|DECLARE, SET, control flow statements are allowed.|  
|6|DECLARE @xact int = XACT_STATE()|-2|We cache error and transaction states for later use.|  
|6|END CATCH|-2||  
|7|-- SELECT @err, @xact|-2|SELECTs are still prohibited here.|  
|8|ROLLBACK|-2|Rolling back the transaction.|  
|9|SELECT @err, @xact|0|No active transactions, SELECTs are allowed.|  
  
## See Also  
[TRY...CATCH &#40;SQL Server PDW&#41;](../sqlpdw/try-catch-sql-server-pdw.md)  
[THROW &#40;SQL Server PDW&#41;](../sqlpdw/throw-sql-server-pdw.md)  
  
