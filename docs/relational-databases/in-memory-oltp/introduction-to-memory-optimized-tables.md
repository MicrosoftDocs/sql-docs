---
title: "Introduction to Memory-Optimized Tables"
description: Learn about memory-optimized tables, which are durable and support transactions that are atomic, consistent, isolated, and durable.
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/23/2025
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: concept-article
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Introduction to Memory-Optimized Tables

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Memory-optimized tables are created using [CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md).  
  
 Memory-optimized tables are fully durable by default, and, like transactions on (traditional) disk-based tables, transactions on memory-optimized tables are fully atomic, consistent, isolated, and durable (ACID). Memory-optimized tables and natively compiled stored procedures support only a subset of [!INCLUDE[tsql](../../includes/tsql-md.md)] features.
 
Starting with SQL Server 2016, and in Azure SQL Database, there are no limitations for [collations or code pages](../../relational-databases/collations/collation-and-unicode-support.md) that are specific to In-Memory OLTP.
  
 The primary storage for memory-optimized tables is the main memory. Rows in the table are read from and written to memory. A second copy of the table data is maintained on disk, but only for durability purposes. See [Creating and Managing Storage for Memory-Optimized Objects](../../relational-databases/in-memory-oltp/creating-and-managing-storage-for-memory-optimized-objects.md) for more information about durable tables. Data in memory-optimized tables is only read from disk during database recovery (for example, after a server restart).  
  
 For even greater performance gains, In-Memory OLTP supports durable tables with transaction durability delayed. Delayed durable transactions are saved to disk soon after the transaction commits and control is returned to the client. In exchange for the increased performance, committed transactions that aren't persisted to disk are lost in a server crash or fail over.  
  
 Besides the default durable memory-optimized tables, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also supports non-durable memory-optimized tables, which aren't logged and their data isn't persisted on disk. This means that transactions on these tables don't require any disk IO, but the data is lost if there is a server crash or failover.  
  
 In-Memory OLTP is integrated with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to provide a seamless experience in all areas such as development, deployment, manageability, and supportability. A database can contain in-memory as well as disk-based objects.  
  
 Rows in memory-optimized tables are versioned. This means that each row in the table potentially has multiple versions. All row versions are maintained in the same table data structure. Row versioning is used to allow concurrent reads and writes on the same row. For more information about concurrent reads and writes on the same row, see [Transactions with Memory-Optimized Tables](../../relational-databases/in-memory-oltp/transactions-with-memory-optimized-tables.md).  
  
 The following figure illustrates multi-versioning. The figure shows a table with three rows and each row has different versions.  
  
![Multi-versioning.](../../relational-databases/in-memory-oltp/media/hekaton-tables-1.gif "Multi-versioning.")  
  
 The table has three rows: r1, r2, and r3. r1 has three versions, r2 has two versions, and r3 has four versions. Different versions of the same row don't necessarily occupy consecutive memory locations. The different row versions can be dispersed throughout the table data structure.  
  
 The memory-optimized table data structure can be seen as a collection of row versions. Rows in disk-based tables are organized in pages and extents, and individual rows addressed using page number and page offset, row versions in memory-optimized tables are addressed using 8-byte memory pointers.  
  
 Data in memory-optimized tables is accessed in two ways:  
  
-   Through natively compiled stored procedures.  
  
-   Through interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)], outside of a natively compiled stored procedure. These [!INCLUDE[tsql](../../includes/tsql-md.md)] statements may be either inside interpreted stored procedures or they may be ad hoc [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
## Accessing Data in Memory-Optimized Tables  

Memory-optimized tables can be accessed most efficiently from natively compiled stored procedures ([Natively Compiled Stored Procedures](./a-guide-to-query-processing-for-memory-optimized-tables.md)). Memory-optimized tables can also be accessed with (traditional) interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)]. Interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] refers to accessing memory-optimized tables without a natively compiled stored procedure. Some examples of interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] access include accessing a memory-optimized table from a DML trigger, ad hoc [!INCLUDE[tsql](../../includes/tsql-md.md)] batch, view, and table-valued function.  

 The following table summarizes native and interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] access for various objects.  
  
|Feature|Access Using a Natively Compiled Stored Procedure|Interpreted [!INCLUDE[tsql](../../includes/tsql-md.md)] Access|CLR Access|  
|-------------|-------------------------------------------------------|-------------------------------------------|----------------|  
|Memory-optimized table|Yes|Yes|No<sup>1</sup>|  
|Memory-optimized table type|Yes|Yes|No|  
|Natively compiled stored procedure|Nesting of natively compiled stored procedures is now supported. You can use the EXECUTE syntax inside the stored procedures, as long as the referenced procedure is also natively compiled.|Yes|No*|  

<sup>1</sup>You can't access a memory-optimized table or natively compiled stored procedure from the context connection (the connection from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when executing a CLR module). You can, however, create and open another connection from which you can access memory-optimized tables and natively compiled stored procedures.  

Sensitive data in memory-optimized tables can be protected by using [Always Encrypted](../security/encryption/always-encrypted-database-engine.md). The following limitations apply:

- When using [Always Encrypted with secure enclaves](../security/encryption/always-encrypted-enclaves.md), the use of enclave-enabled keys for columns in memory-optimized tables isn't supported. This means that in-place encryption can't be used, and the initial encryption is done on the client.
- Always Encrypted isn't supported for any column in a memory-optimized table when the table is referenced in a natively compiled module.

## Performance and Scalability  

The following factors affect the performance gains that can be achieved with In-Memory OLTP:  
  
*Communication:* An application using many short stored procedure calls might see a smaller performance gain compared to an application with fewer calls and more functionality implemented in each stored procedure.  
  
*[!INCLUDE[tsql](../../includes/tsql-md.md)] Execution:* In-Memory OLTP achieves the best performance when using natively compiled stored procedures rather than interpreted stored procedures or query execution. There can be a benefit to accessing memory-optimized tables from such stored procedures.  
  
*Range Scan vs Point Lookup:* Memory-optimized nonclustered indexes support range scans and ordered scans. For point lookups, memory-optimized hash indexes have better performance than memory-optimized nonclustered indexes. Memory-optimized nonclustered indexes have better performance than disk-based indexes.

- Starting in SQL Server 2016, the query plan for a memory-optimized table can scan the table in parallel. This improves the performance of analytical queries.
  - Hash indexes also became scannable in parallel in SQL Server 2016.
  - Nonclustered indexes also became scannable in parallel in SQL Server 2016.
  
*Index operations:* Index operations aren't logged, and they exist only in memory.  
  
*Concurrency:* Applications whose performance is affected by engine-level concurrency, such as latch contention or blocking, improves significantly when the application moves to In-Memory OLTP.  
  
The following table lists the performance and scalability issues that are commonly found in relational databases and how In-Memory OLTP can improve performance.  
  
|Issue|In-Memory OLTP Impact|  
|-----------|----------------------------|  
|Performance<br /><br /> High resource (CPU, I/O, network, or memory) usage.|CPU<br /> Natively compiled stored procedures can lower CPU usage significantly because they require fewer instructions to execute a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement compared to interpreted stored procedures.<br /><br /> In-Memory OLTP can help reduce the hardware investment in scaled-out workloads because one server can potentially deliver the throughput of multiple servers.<br /><br /> I/O<br /> If you encounter an I/O bottleneck from processing to data or index pages, In-Memory OLTP may reduce the bottleneck. Additionally, the checkpointing of In-Memory OLTP objects is continuous and doesn't lead to sudden increases in I/O operations. However, if the working set of the performance critical tables doesn't fit in memory, In-Memory OLTP doesn't apply because it requires data to be memory resident. If you encounter an I/O bottleneck in logging, In-Memory OLTP can reduce the bottleneck because it does less logging. If one or more memory-optimized tables are configured as non-durable tables, you can eliminate logging for data.<br /><br /> Memory<br /> In-Memory OLTP doesn't offer any performance benefit. In-Memory OLTP can put extra pressure on memory as the objects need to be memory resident.<br /><br /> Network<br /> In-Memory OLTP doesn't offer any performance benefit. The data needs to be communicated from data tier to application tier.|  
|Scalability<br /><br /> Most scaling issues in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] applications are caused by concurrency issues such as contention in locks, latches, and spinlocks.|Latch Contention<br /> A typical scenario is contention on the last page of an index when inserting rows concurrently in key order. Because In-Memory OLTP doesn't take latches when accessing data, the scalability issues related to latch contentions are fully removed.<br /><br /> Spinlock Contention<br /> Because In-Memory OLTP doesn't take latches when accessing data, the scalability issues related to spinlock contentions are fully removed.<br /><br /> Locking Related Contention<br /> If your database application encounters blocking issues between read and write operations, In-Memory OLTP removes the blocking issues because it uses a new form of optimistic concurrency control to implement all transaction isolation levels. In-Memory OLTP doesn't use TempDB to store row versions.<br /><br /> If the scaling issue is caused by conflict between two write operations, such as two concurrent transactions trying to update the same row, In-Memory OLTP lets one transaction succeed and fails the other transaction. The failed transaction must be resubmitted either explicitly or implicitly, retrying the transaction. In either case, you need to make changes to the application.<br /><br /> If your application experiences frequent conflicts between two write operations, the value of optimistic locking is diminished. The application isn't suitable for In-Memory OLTP. Most OLTP applications don't have write conflicts unless the conflict is the result of lock escalation.|  
  
##  <a name="rls"></a> Row-Level Security in Memory-Optimized Tables  

[Row-Level Security](../../relational-databases/security/row-level-security.md) is supported in memory-optimized tables. Applying Row-Level Security policies to memory-optimized tables is essentially the same as disk-based tables, except that the inline table-valued functions used as security predicates must be natively compiled (created using the WITH NATIVE_COMPILATION option). For details, see the [Cross-Feature Compatibility](../../relational-databases/security/row-level-security.md#Limitations) section in the [Row-Level Security](../../relational-databases/security/row-level-security.md) topic.  
  
 Various built-in security functions that are essential to row-level security are available for memory-optimized tables. For details, see [Built-in Functions in Natively Compiled Modules](../../relational-databases/in-memory-oltp/supported-features-for-natively-compiled-t-sql-modules.md#bfncsp).  
  
 **EXECUTE AS CALLER** - All native modules now support and use EXECUTE AS CALLER by default, even if the hint isn't specified. This is because it is expected that all row-level security predicate functions use EXECUTE AS CALLER so that the function, and any built-in functions used within it, are evaluated in the context of the calling user.   
EXECUTE AS CALLER has a small (approximately10%) performance hit caused by permission checks on the caller. If the module specifies EXECUTE AS OWNER or EXECUTE AS SELF explicitly, these permission checks and their associated performance cost is avoided. However, using either of these options together with the mentioned built-in functions incurs a higher performance hit due to the necessary context-switching.  
  
## Scenarios

For a brief discussion of typical scenarios where [!INCLUDE[inmemory-md](../../includes/inmemory-md.md)] can improve performance, see [In-Memory OLTP](./overview-and-usage-scenarios.md).  
  
## See Also

[In-Memory OLTP &#40;In-Memory Optimization&#41;](./overview-and-usage-scenarios.md)  
