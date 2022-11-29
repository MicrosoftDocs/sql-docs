---
title: "Dealing with Failed Updates"
description: "Dealing with Failed Updates"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "updates [ADO], dealing with failed updates"
---
# Dealing with Failed Updates
When an update concludes with errors, how you resolve the errors depends on the nature and severity of the errors and the logic of your application. However, if the database is shared with other users, a typical error is that someone else modifies the field before you do. This type of error is called a conflict. ADO detects this situation and reports an error.  
  
## Remarks  
 If there are update errors, they will be trapped in an error-handling routine. Filter the Recordset with the adFilterConflictingRecords constant so that only the conflicting rows are visible. In this example, the error-resolution strategy is merely to print the author's first and last names (au_fname and au_lname).  
  
 The code to alert the user to the update conflict looks like this:  
  
```  
objRs.Filter = adFilterConflictingRecords  
objRs.MoveFirst  
Do While Not objRst.EOF  
   Debug.Print "Conflict: Name =  "; objRs!au_fname; " "; objRs!au_lname  
   objRs.MoveNext  
Loop  
```  
  
## See Also  
 [Batch Mode](./batch-mode.md)