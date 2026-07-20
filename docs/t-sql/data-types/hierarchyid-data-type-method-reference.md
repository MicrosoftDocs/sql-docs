---
title: "Data type method reference for hierarchyid (Transact-SQL)"
description: hierarchyid data type method reference
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 11/03/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "hierarchyid"
  - "hierarchyid_TSQL"
helpviewer_keywords:
  - "Hierarchy data type"
  - "hierarchyid data type"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# hierarchyid data type method reference

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The **hierarchyid** data type is a variable length, system data type. Use **hierarchyid** to represent position in a hierarchy. A column of type **hierarchyid** doesn't automatically represent a tree. It's up to the application to generate and assign **hierarchyid** values in such a way that the desired relationship between rows is reflected in the values.

A value of the **hierarchyid** data type represents a position in a tree hierarchy. Values for **hierarchyid** have the following properties:

- **Extremely compact**: The average number of bits that are required to represent a node in a tree with *n* nodes depends on the average fanout (the average number of children of a node). For small fanouts (0-7), the size is about 6 \* logA *n* bits, where A is the average fanout. A node in an organizational hierarchy of 100,000 people with an average fanout of six levels takes about 38 bits. This is rounded up to 40 bits, or 5 bytes, for storage.

- **Comparison is in depth-first order**: Given two **hierarchyid** values `a` and `b`, `a<b` means a comes before b in a depth-first traversal of the tree. Indexes on **hierarchyid** data types are in depth-first order, and nodes close to each other in a depth-first traversal are stored near each other. For example, the children of a record are stored adjacent to that record. For more information, see [Hierarchical data (SQL Server)](../../relational-databases/hierarchical-data-sql-server.md).

- **Support for arbitrary insertions and deletions**: By using the [GetDescendant](getdescendant-database-engine.md) method, it's always possible to generate a sibling to the right of any given node, to the left of any given node, or between any two siblings. The comparison property is maintained when an arbitrary number of nodes is inserted or deleted from the hierarchy. Most insertions and deletions preserve the compactness property. However, insertions between two nodes produce hierarchyid values with a slightly less compact representation.

- **Encoding is limited to 892 bytes:** So, nodes that have too many levels in their representation to fit into 892 bytes can't be represented by the **hierarchyid** type.

The **hierarchyid** type is available to common language runtime (CLR) clients as the `SqlHierarchyId` data type.

## Remarks

The **hierarchyid** type logically encodes information about a single node in a hierarchy tree by encoding the path from the root of the tree to the node. Such a path is logically represented as a sequence of node labels of all children visited after the root. A slash starts the representation, and a path that only visits the root is represented by a single slash. For levels underneath the root, each label is encoded as a sequence of integers separated by dots.

Comparison between children is performed by comparing the integer sequences separated by dots in dictionary order. Each level is followed by a slash. Therefore a slash separates parents from their children. For example, the following are valid **hierarchyid** paths of lengths 1, 2, 2, 3, and 3 levels respectively:

- `/`
- `/1/`
- `/0.3.-7/`
- `/1/3/`
- `/0.1/0.2/`

Nodes can be inserted in any location. Nodes inserted after `/1/2/` but before `/1/3/` can be represented as `/1/2.5/`. Nodes inserted before `0` have the logical representation as a negative number. For example, a node that comes before `/1/1/` can be represented as `/1/-1/`. Nodes can't have leading zeros. For example, `/1/1.1/` is valid, but `/1/1.01/` isn't valid. To prevent errors, insert nodes by using the [GetDescendant](getdescendant-database-engine.md) method.

## Data type conversion

The **hierarchyid** data type can be converted to other data types as follows:

- Use the [ToString](tostring-database-engine.md) method to convert the **hierarchyid** value to the logical representation as a **nvarchar(4000)** data type.

- Use [Read (Database Engine) by using CSharp](read-database-engine.md) and [Write](write-database-engine.md) to convert **hierarchyid** to **varbinary**.

- To transmit **hierarchyid** parameters through SOAP, first cast them as strings.

## Upgrade databases

When a database is upgraded to a newer version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], the new assembly and the **hierarchyid** data type are automatically installed. Upgrade advisor rules detect any user type or assemblies with conflicting names. The upgrade advisor advises renaming of any conflicting assembly, and either renaming any conflicting type, or using two-part names in the code to refer to that preexisting user type.

If a database upgrade detects a user assembly with conflicting name, it automatically renames that assembly and put the database into suspect mode.

If a user type with conflicting name exists during the upgrade, no special steps are taken. After the upgrade, both the old user type and the new system type exist. The user type is available only through two-part names.

<a id="using-hierarchyid-columns-in-replicated-tables"></a>

## Use hierarchyid columns in replicated tables

Columns of type **hierarchyid** can be used on any replicated table. The requirements for your application depend on whether replication is one directional or bidirectional, and on the versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that are used.

### One-directional replication

One-directional replication includes snapshot replication, transactional replication, and merge replication in which changes aren't made at the Subscriber. How **hierarchyid** columns work with one directional replication depends on the version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] the Subscriber is running.

- A [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Publisher can replicate **hierarchyid** columns to a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Subscriber of the same version without any special considerations.

- A [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Publisher must convert **hierarchyid** columns to replicate them to a Subscriber that's running [!INCLUDE [ssEW](../../includes/ssew-md.md)] or an earlier version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE [ssEW](../../includes/ssew-md.md)] and earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] don't support **hierarchyid** columns. If you're using one of these versions, you can still replicate data to a Subscriber. To do this, you must set a schema option or the publication compatibility level (for merge replication) so the column can be converted to a compatible data type.

Column filtering is supported in both of these scenarios. This includes filtering out **hierarchyid** columns. Row filtering is supported as long as the filter doesn't include a **hierarchyid** column.

### Bi-directional replication

Bi-directional replication includes transactional replication with updating subscriptions, peer-to-peer transactional replication, and merge replication in which changes are made at the Subscriber. Replication lets you configure a table with **hierarchyid** columns for bi-directional replication. Note the following requirements and considerations.

- The Publisher and all Subscribers must be running the same version, on [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] or a later version.

- Replication replicates the data as bytes and doesn't perform any validation to assure the integrity of the hierarchy.

- The hierarchy of the changes that were made at the source (Subscriber or Publisher) isn't maintained when they replicate to the destination.

- The values for **hierarchyid** columns can have identical binary representations across all databases. Conflicts can occur in bi-directional replication when application logic independently generates the same **hierarchyid** for different entities. Therefore, the same value could be generated on the Publisher and Subscriber, but it could be applied to different rows. Replication doesn't check for this condition, and there's no built-in way to partition **hierarchyid** column values as there's for `IDENTITY` columns. Applications must use constraints or other mechanisms to avoid such undetected conflicts.

- It's possible that rows that are inserted on the Subscriber can be orphaned. The parent row of the inserted row might be deleted at the Publisher. This results in an undetected conflict when the row from the Subscriber is inserted at the Publisher.

- Column filters can't filter out non-nullable **hierarchyid** columns. Inserts from the Subscriber fail because there's no default value for the **hierarchyid** column on the Publisher.

- Row filtering is supported as long as the filter doesn't include a **hierarchyid** column.

## Related content

- [Hierarchical data (SQL Server)](../../relational-databases/hierarchical-data-sql-server.md)
