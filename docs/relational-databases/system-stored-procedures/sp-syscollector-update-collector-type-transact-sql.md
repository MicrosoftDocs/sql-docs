---
title: "sp_syscollector_update_collector_type (Transact-SQL)"
description: Updates a collector type for a collection item.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_syscollector_update_collector_type_TSQL"
  - "sp_syscollector_update_collector_type"
helpviewer_keywords:
  - "sp_syscollector_update_collector_type"
  - "data collector [SQL Server], stored procedures"
dev_langs:
  - "TSQL"
---
# sp_syscollector_update_collector_type (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Updates a collector type for a collection item. Given the name and GUID of a collector type, updates the collector type configuration, including the collection and upload package, the parameter schema, and the parameter formatter schema.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_syscollector_update_collector_type
    [ [ @collector_type_uid = ] 'collector_type_uid' ]
    [ , [ @name = ] N'name' ]
    [ , [ @parameter_schema = ] N'parameter_schema' ]
    [ , [ @parameter_formatter = ] N'parameter_formatter' ]
    , [ @collection_package_id = ] 'collection_package_id'
    , [ @upload_package_id = ] 'upload_package_id'
[ ; ]
```

## Arguments

#### [ @collector_type_uid = ] '*collector_type_uid*'

The GUID for the collector type. *@collector_type_uid* is **uniqueidentifier**, and if it's `NULL`, *@collector_type_uid* is automatically created and returned as OUTPUT.

#### [ @name = ] N'*name*'

The name of the collector type. *@name* is **sysname**, and must be specified.

#### [ @parameter_schema = ] N'*parameter_schema*'

The XML schema for this collector type. *@parameter_schema* is **xml**, with a default of `NULL`, and may be required by certain collector types. If it isn't required, this argument can be `NULL`.

#### [ @parameter_formatter = ] N'*parameter_formatter*'

The template to use to transform the XML for use in the collection set property page. *@parameter_formatter* is **xml**, with a default of `NULL`.

#### [ @collection_package_id = ] '*collection_package_id*'

A local unique identifier that points to the [!INCLUDE [ssIS](../../includes/ssis-md.md)] collection package used by the collection set. *@collection_package_id* is **uniqueidentifier**, with no default, and is required. To obtain the value for *@collection_package_id*, query the dbo.syscollector_collector_types system view in the `msdb` database.

#### [ @upload_package_id = ] '*upload_package_id*'

A local unique identifier that points to the [!INCLUDE [ssIS](../../includes/ssis-md.md)] upload package used by the collection set. *@upload_package_id* is **uniqueidentifier**, with no default, and is required. To obtain the value for *@upload_package_id*, query the dbo.syscollector_collector_types system view in the `msdb` database.

## Return code values

`0` (success) or `1` (failure).

## Permissions

Requires membership in the **dc_admin** (with EXECUTE permission) fixed database role.

## Examples

This example updates the Generic T-SQL Query collector type. (In the example, the default schema for the Generic T-SQL Query collector type is used.)

```sql
USE msdb;
GO

EXEC sp_syscollector_update_collector_type @collector_type_uid = '302E93D1-3424-4BE7-AA8E-84813ECF2419',
    @name = 'Generic T-SQL Query Collector Type',
    @parameter_schema = '<?xml version="1.0" encoding="utf-8"?>
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema" targetNamespace="DataCollectorType">
  <xs:element name="TSQLQueryCollector">
<xs:complexType>
  <xs:sequence>
<xs:element name="Query" minOccurs="1" maxOccurs="unbounded">
  <xs:complexType>
<xs:sequence>
  <xs:element name="Value" type="xs:string" />
  <xs:element name="OutputTable" type="xs:string" />
</xs:sequence>
  </xs:complexType>
</xs:element>
<xs:element name="Databases" minOccurs="0" maxOccurs="1">
  <xs:complexType>
<xs:sequence>
  <xs:element name="Database" minOccurs="0" maxOccurs="unbounded" type="xs:string" />
</xs:sequence>
<xs:attribute name="UseSystemDatabases" type="xs:boolean" use="optional" />
<xs:attribute name="UseUserDatabases" type="xs:boolean" use="optional" />
  </xs:complexType>
</xs:element>
  </xs:sequence>
</xs:complexType>
  </xs:element>
</xs:schema>',
    @collection_package_id = '292B1476-0F46-4490-A9B7-6DB724DE3C0B',
    @upload_package_id = '6EB73801-39CF-489C-B682-497350C939F0';
GO
```

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Data collection](../data-collection/data-collection.md)
