---
title: "CREATE MESSAGE TYPE (Transact-SQL)"
description: CREATE MESSAGE TYPE (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "04/10/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "CREATE_MESSAGE_TSQL"
  - "MESSAGE_TSQL"
  - "MESSAGE"
  - "CREATE MESSAGE"
  - "CREATE_MESSAGE_TYPE_TSQL"
  - "MESSAGE TYPE"
  - "MESSAGE_TYPE_TSQL"
  - "CREATE MESSAGE TYPE"
helpviewer_keywords:
  - "XML [Service Broker]"
  - "validation [Service Broker]"
  - "message types [Service Broker], creating"
  - "empty messages [SQL Server]"
  - "binary [SQL Server], message types"
  - "CREATE MESSAGE TYPE statement"
dev_langs:
  - "TSQL"
---
# CREATE MESSAGE TYPE (Transact-SQL)
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

  Creates a new message type. A message type defines the name of a message and the validation that [!INCLUDE[ssSB](../../includes/sssb-md.md)] performs on messages that have that name. Both sides of a conversation must define the same message types.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
CREATE MESSAGE TYPE message_type_name  
    [ AUTHORIZATION owner_name ]  
    [ VALIDATION = {  NONE  
                    | EMPTY   
                    | WELL_FORMED_XML  
                    | VALID_XML WITH SCHEMA COLLECTION schema_collection_name  
                   } ]  
[ ; ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *message_type_name*  
 Is the name of the message type to create. A new message type is created in the current database and owned by the principal specified in the AUTHORIZATION clause. Server, database, and schema names cannot be specified. The *message_type_name* can be up to 128 characters.  
  
 AUTHORIZATION *owner_name*  
 Sets the owner of the message type to the specified database user or role. When the current user is **dbo** or **sa**, *owner_name* can be the name of any valid user or role. Otherwise, *owner_name* must be the name of the current user, the name of a user who the current user has IMPERSONATE permission for, or the name of a role to which the current user belongs. When this clause is omitted, the message type belongs to the current user.  
  
 VALIDATION  
 Specifies how [!INCLUDE[ssSB](../../includes/sssb-md.md)] validates the message body for messages of this type. When this clause is not specified, validation defaults to NONE.  
  
 NONE  
 Specifies that no validation is performed. The message body can contain data, or it can be NULL.  
  
 EMPTY  
 Specifies that the message body must be NULL.  
  
 WELL_FORMED_XML  
 Specifies that the message body must contain well-formed XML.  
  
 VALID_XML WITH SCHEMA COLLECTION *schema_collection_name*  
 Specifies that the message body must contain XML that complies with a schema in the specified schema collection The *schema_collection_name* must be the name of an existing XML schema collection.  
  
## Remarks  
 [!INCLUDE[ssSB](../../includes/sssb-md.md)] validates incoming messages. When a message contains a message body that does not comply with the validation type specified, [!INCLUDE[ssSB](../../includes/sssb-md.md)] discards the invalid message and returns an error message to the service that sent the message.  
  
 Both sides of a conversation must define the same name for a message type. To help troubleshooting, both sides of a conversation typically specify the same validation for the message type, although [!INCLUDE[ssSB](../../includes/sssb-md.md)] does not require that both sides of the conversation use the same validation.  
  
 A message type can not be a temporary object. Message type names starting with **#** are allowed, but are permanent objects.  
  
## Permissions  
 Permission for creating a message type defaults to members of the **db_ddladmin** or **db_owner** fixed database roles and the **sysadmin** fixed server role.  
  
 REFERENCES permission for a message type defaults to the owner of the message type, members of the **db_owner** fixed database role, and members of the **sysadmin** fixed server role.  
  
 When the CREATE MESSAGE TYPE statement specifies a schema collection, the user executing the statement must have REFERENCES permission on the schema collection specified.  
  
## Examples  
  
### A. Creating a message type containing well-formed XML  
 The following example creates a new message type that contains well-formed XML.  
  
```sql  
CREATE MESSAGE TYPE  
  [//Adventure-Works.com/Expenses/SubmitExpense]  
  VALIDATION = WELL_FORMED_XML ;     
```  
  
### B. Creating a message type containing typed XML  
 The following example creates a message type for an expense report encoded in XML. The example creates an XML schema collection that holds the schema for a simple expense report. The example then creates a new message type that validates messages against the schema.  
  
```sql  
CREATE XML SCHEMA COLLECTION ExpenseReportSchema AS  
N'<?xml version="1.0" encoding="UTF-16" ?>  
  <xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"  
     targetNamespace="https://Adventure-Works.com/schemas/expenseReport"  
     xmlns:expense="https://Adventure-Works.com/schemas/expenseReport"  
     elementFormDefault="qualified"  
   >   
    <xsd:complexType name="expenseReportType">  
       <xsd:sequence>  
         <xsd:element name="EmployeeName" type="xsd:string"/>  
         <xsd:element name="EmployeeID" type="xsd:string"/>  
         <xsd:element name="ItemDetail"  
           type="expense:ItemDetailType" maxOccurs="unbounded"/>  
      </xsd:sequence>  
    </xsd:complexType>  
  
    <xsd:complexType name="ItemDetailType">  
      <xsd:sequence>  
        <xsd:element name="Date" type="xsd:date"/>  
        <xsd:element name="CostCenter" type="xsd:string"/>  
        <xsd:element name="Total" type="xsd:decimal"/>  
        <xsd:element name="Currency" type="xsd:string"/>  
        <xsd:element name="Description" type="xsd:string"/>  
      </xsd:sequence>  
    </xsd:complexType>  
  
    <xsd:element name="ExpenseReport" type="expense:expenseReportType"/>  
  
  </xsd:schema>' ;  
  
  CREATE MESSAGE TYPE  
    [//Adventure-Works.com/Expenses/SubmitExpense]  
    VALIDATION = VALID_XML WITH SCHEMA COLLECTION ExpenseReportSchema ;  
```  
  
### C. Creating a message type for an empty message  
 The following example creates a new message type with empty encoding.  
  
```sql  
CREATE MESSAGE TYPE  
    [//Adventure-Works.com/Expenses/SubmitExpense]  
    VALIDATION = EMPTY ;  
```  
  
### D. Creating a message type containing binary data  
 The following example creates a new message type to hold binary data. Because the message will contain data that is not XML, the message type specifies a validation type of `NONE`. Notice that, in this case, the application that receives a message of this type must verify that the message contains data, and that the data is of the type expected.  
  
```sql  
CREATE MESSAGE TYPE  
    [//Adventure-Works.com/Expenses/ReceiptImage]  
    VALIDATION = NONE ;  
```  
  
## See Also  
 [ALTER MESSAGE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-message-type-transact-sql.md)   
 [DROP MESSAGE TYPE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-message-type-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
