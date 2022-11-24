---
title: "GetReparentedValue (Database Engine)"
description: "GetReparentedValue (Database Engine)"
author: MikeRayMSFT
ms.author: mikeray
ms.date: "07/22/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "Reparent_TSQL"
  - "Reparent"
helpviewer_keywords:
  - "Reparent [Database Engine]"
dev_langs:
  - "TSQL"
---
# GetReparentedValue (Database Engine)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a node whose path from the root is the path to _newRoot_, followed by the path from _oldRoot_.
  
## Syntax  
  
```syntaxsql
-- Transact-SQL syntax  
node. GetReparentedValue ( oldRoot, newRoot )  
```  
  
```syntaxsql
-- CLR syntax  
SqlHierarchyId GetReparentedValue ( SqlHierarchyId oldRoot , SqlHierarchyId newRoot )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
_oldRoot_  
A **hierarchyid** that is the node that represents the level of the hierarchy that is to be modified.
  
_newRoot_  
A **hierarchyid** that represents the node. Replace the _oldRoot_ section of the current node to move the node.
  
## Return Types  
**SQL Server return type:hierarchyid**
  
**CLR return type:SqlHierarchyId**
  
## Remarks  
Used to modify the tree by moving nodes from _oldRoot_ to _newRoot_. GetReparentedValue is used to move a hierarchy node to a new location in the hierarchy. The **hierarchyid** data type represents but doesn't enforce the hierarchical structure. Users must ensure that the hierarchyid is appropriately structured for the new location. A unique index on the **hierarchyid** data type can help prevent duplicate entries. For an example of moving an entire subtree, see [Hierarchical Data &#40;SQL Server&#41;](../../relational-databases/hierarchical-data-sql-server.md).
  
## Examples  
  
### A. Comparing two node locations  
The following example shows the current hierarchyid of a node. It also shows what the **hierarchyid** of the node would be if you move the node to become a descendant of the **\@NewParent** node. It uses the `ToString()` method to show the hierarchical relationships.
  
```sql
DECLARE @SubjectEmployee hierarchyid , @OldParent hierarchyid, @NewParent hierarchyid  
SELECT @SubjectEmployee = OrgNode FROM HumanResources.EmployeeDemo  
  WHERE LoginID = 'adventure-works\gail0' ;  
SELECT @OldParent = OrgNode FROM HumanResources.EmployeeDemo  
  WHERE LoginID = 'adventure-works\roberto0' ; -- who is /1/1/  
SELECT @NewParent = OrgNode FROM HumanResources.EmployeeDemo  
  WHERE LoginID = 'adventure-works\wanida0' ; -- who is /2/3/  
  
SELECT OrgNode.ToString() AS Current_OrgNode_AS_Text,   
(@SubjectEmployee. GetReparentedValue(@OldParent, @NewParent) ).ToString() AS Proposed_OrgNode_AS_Text,  
OrgNode AS Current_OrgNode,  
@SubjectEmployee. GetReparentedValue(@OldParent, @NewParent) AS Proposed_OrgNode,  
*  
FROM HumanResources.EmployeeDemo  
WHERE OrgNode = @SubjectEmployee ;  
GO  
```  
  
### B. Updating a node to a new location  
The following example uses `GetReparentedValue()` in an UPDATE statement to move a node from an old location to a new location in the hierarchy:
  
```sql
DECLARE @SubjectEmployee hierarchyid , @OldParent hierarchyid, @NewParent hierarchyid  
SELECT @SubjectEmployee = OrgNode FROM HumanResources.EmployeeDemo  
  WHERE LoginID = 'adventure-works\gail0' ; -- Node /1/1/2/  
SELECT @OldParent = OrgNode FROM HumanResources.EmployeeDemo  
  WHERE LoginID = 'adventure-works\roberto0' ; -- Node /1/1/  
SELECT @NewParent = OrgNode FROM HumanResources.EmployeeDemo  
  WHERE LoginID = 'adventure-works\wanida0' ; -- Node /2/3/  
  
UPDATE HumanResources.EmployeeDemo  
SET OrgNode = @SubjectEmployee. GetReparentedValue(@OldParent, @NewParent)   
WHERE OrgNode = @SubjectEmployee ;  
  
SELECT OrgNode.ToString() AS Current_OrgNode_AS_Text,   
*  
FROM HumanResources.EmployeeDemo  
WHERE LoginID = 'adventure-works\gail0' ; -- Now node /2/3/2/  
```  
  
### C. CLR example  
The following code snippet calls the GetReparentedValue () method:
  
```sql
this. GetReparentedValue(oldParent, newParent)  
```  
  
## See also
[hierarchyid Data Type Method Reference](./hierarchyid-data-type-method-reference.md)  
[Hierarchical Data &#40;SQL Server&#41;](../../relational-databases/hierarchical-data-sql-server.md)  
[hierarchyid &#40;Transact-SQL&#41;](../../t-sql/data-types/hierarchyid-data-type-method-reference.md)
  
