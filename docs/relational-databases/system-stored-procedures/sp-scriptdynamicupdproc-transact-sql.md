---
title: "sp_scriptdynamicupdproc (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "sp_scriptdynamicupdproc_TSQL"
  - "sp_scriptdynamicupdproc"
helpviewer_keywords: 
  - "sp_scriptdynamicupdproc"
ms.assetid: b4c18863-ed92-4aa2-a04f-7ed832fc9e07
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_scriptdynamicupdproc (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Generates the CREATE PROCEDURE statement that creates a dynamic update stored procedure. The UPDATE statement within the custom stored procedure is built dynamically based on the MCALL syntax that indicates which columns to change. Use this stored procedure if the number of indexes on the subscribing table is growing and the number of columns being changed is small. This stored procedure is run at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_scriptdynamicupdproc [ @artid =] artid  
```  
  
## Arguments  
 [ **@artid=**] *artid*  
 Is the article ID. *artid* is **int**, with no default.  
  
## Result Sets  
 Returns a result set that consists of a single **nvarchar(4000)** column. The result set forms the complete CREATE PROCEDURE statement used to create the custom stored procedure.  
  
## Remarks  
 **sp_scriptdynamicupdproc** is used in transactional replication. The default MCALL scripting logic includes all columns within the UPDATE statement and uses a bitmap to determine the columns that have changed. If a column did not change, the column is set back to itself, which usually causes no problems. If the column is indexed, extra processing occurs. The dynamic approach includes only the columns that have changed, which provides an optimal UPDATE string. However, extra processing is incurred at runtime when the dynamic UPDATE statement is built. We recommend that you test the dynamic and static approaches, and then choose the optimal solution.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_scriptdynamicupdproc**.  
  
## Examples  
 This example creates an article (with *artid* set to **1**) on the **authors** table in the **pubs** database and specifies that the UPDATE statement is the custom procedure to execute:  
  
```  
'MCALL sp_mupd_authors'  
```  
  
 Generate the custom stored procedures to be executed by the Distribution Agent at the Subscriber by running the following stored procedure at the Publisher:  
  
```  
EXEC sp_scriptdynamicupdproc @artid = '1'  
  
The statement returns:  
  
CREATE PROCEDURE [sp_mupd_authors]   
  @c1 varchar(11),@c2 varchar(40),@c3 varchar(20),@c4 char(12),@c5 varchar(40),@c6 varchar(20),  
  @c7 char(2),@c8 char(5),@c9 bit,@pkc1 varchar(11),@bitmap binary(2)  
as  
  
declare @stmt nvarchar(4000), @spacer nvarchar(1)  
SELECT @spacer =N''  
SELECT @stmt = N'UPDATE [authors] SET '  
  
if substring(@bitmap,1,1) & 2 = 2  
begin  
  select @stmt = @stmt + @spacer + N'[au_lname]' + N'=@2'  
  select @spacer = N','  
end  
if substring(@bitmap,1,1) & 4 = 4  
begin  
  select @stmt = @stmt + @spacer + N'[au_fname]' + N'=@3'  
  select @spacer = N','  
end  
if substring(@bitmap,1,1) & 8 = 8  
begin  
  select @stmt = @stmt + @spacer + N'[phone]' + N'=@4'  
  select @spacer = N','  
end  
if substring(@bitmap,1,1) & 16 = 16  
begin  
  select @stmt = @stmt + @spacer + N'[address]' + N'=@5'  
  select @spacer = N','  
end  
if substring(@bitmap,1,1) & 32 = 32  
begin  
  select @stmt = @stmt + @spacer + N'[city]' + N'=@6'  
  select @spacer = N','  
end  
if substring(@bitmap,1,1) & 64 = 64  
begin  
  select @stmt = @stmt + @spacer + N'[state]' + N'=@7'  
  select @spacer = N','  
end  
if substring(@bitmap,1,1) & 128 = 128  
begin  
  select @stmt = @stmt + @spacer + N'[zip]' + N'=@8'  
  select @spacer = N','  
end  
if substring(@bitmap,2,1) & 1 = 1  
begin  
  select @stmt = @stmt + @spacer + N'[contract]' + N'=@9'  
  select @spacer = N','  
end  
select @stmt = @stmt + N' where [au_id] = @1'  
exec sp_executesql @stmt, N' @1 varchar(11),@2 varchar(40),@3 varchar(20),@4 char(12),@5 varchar(40),  
                             @6 varchar(20),@7 char(2),@8 char(5),@9 bit',@pkc1,@c2,@c3,@c4,@c5,@c6,@c7,@c8,@c9  
  
if @@rowcount = 0  
   if @@microsoftversion>0x07320000  
      exec sp_MSreplraiserror 20598  
```  
  
 After running this stored procedure, you can use the resulting script to create the stored procedure manually at the Subscribers.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
