---
title: "ADO Glossary Terms | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/08/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords:
  - "ADO, glossary"
ms.assetid: b0478836-4123-4357-969a-c5784fc28be5
author: MightyPen
ms.author: genemi
manager: craigg
---
# ADO Glossary Terms
This topic defines terms relevant to ADO.

## A
 absolute URL
 A fully-qualified URL that specifies the location of a resource that resides on the Internet or an intranet. See also *URL* and *relative URL*.

 ActiveX control
 Self-registering, in-process COM component that often has a visual element either at design time or run time. ActiveX controls also have the ability to communicate with an Active Document container, such as Microsoft Internet Explorer.

 ADISAPI (Advanced Data Internet Server Application Programming Interface)
 An ISAPI DLL that provides parsing, Automation control, Recordset marshaling, and MIME packaging. The ADISAPI component works through the API provided by Internet Information Services (IIS). See also *ISAPI*.

 aggregate function
 In a query, a function such as COUNT, AVG, or STDEV that calculates a value using all the rows in a column of a table. In writing expressions and in programming, you can use SQL aggregate functions (including the three listed above) and domain aggregate functions to determine various statistics.

 alias
 An alternate name you give to a column or expression in an SQL SELECT statement, often shorter or more meaningful. For example, BobSales is the alias in the following SELECT statement: "Select wr-Sales as BobSales from SalesDB". An alias can be used to dynamically assign columns to control bindings on the DataControl object.

 apartment threading
 A COM threading model where all calls to an object occur on one thread. In apartment threading, COM synchronizes and marshals calls. See also *COMmddefcom*.

 asynchronous operation
 An operation that returns control to the calling program without waiting for the operation to complete. Before the operation is complete, code execution continues. See also *synchronous operation*.

## B
 binding entry
 A mapping between a field in a table and a variable. In the ADO Visual C++ extensions, **Recordset** fields are mapped to C/C++ variables.

 bitmask
 A numeric value intended for a bit-by-bit value comparison with other numeric values, typically to flag options in parameter or return values. Usually this comparison is done with bitwise logical operators, such as **And** and **Or** in Visual Basic, **&** and **&#124;** in C++.

 For example, the ADO **FieldAttributeEnum** values can be used as bitmasks to determine the attributes of a field. Suppose you wanted to determine if a field was updateable. You could test for this with the following expression in Visual Basic:`Field.Attributes AND adFldUpdatable`

 If the result is TRUE, then the field is updateable.

 bookmark
 A marker that uniquely identifies a row within a set of rows so that a user can quickly navigate to it.

 business object
 An object that performs a defined set of operations, such as data validation or business rule logic. Business objects usually reside on the middle tier.

 business rule
 The combination of validation edits, logon verifications, database lookups, policies, and algorithmic transformations that constitute an enterprise's way of doing business. Also known as *business logic*.

## C
 calculated expression
 An expression that is not constant, but whose value depends upon other values. To be evaluated, a calculated expression must obtain and compute values from other sources, typically in other fields or rows.

 chapter
 A reference to a range of rows from a data source. In ADO, a chapter is typically a reference to another **Recordset**.

 Chapter columns make it possible to define a *parent-child* relationship where the *parent* is the **Recordset** containing the chapter column and the *child* is the **Recordset** represented by the chapter.

 chapter-alias
 An alias that refers to the column appended to the parent.

 character set
 A mapping of a set of characters to their numeric values. For example, Unicode is a 16-bit character set capable of encoding all known characters and used as a worldwide character-encoding standard.

 child
 The dependant side of a hierarchical relationship. A child is a node in a hierarchical structure that has another node above it (closer to the root). See also *child-alias*, *parent-child relationship*, *parent*.

 child-alias
 An alias that refers to the child. See also *alias*, *child*.

 CLSID (class identifier)
 A universally unique identifier (UUID) that identifies a COM component. Each COM component has its CLSID in the Windows Registry so that it can be loaded by other applications. See also *ProgID*, *COM*.

 client tier
 A logical layer of a distributed system that typically presents data to and processes input from the user, sometimes referred to as the *front end*. Usually, the client tier requests data from a server based on input, and then formats and displays the result. See also *middle tier*, *data source tier*, *distributed application*.

 COM (Component Object Model)
 A binary standard that enables objects to interoperate in a networked environment regardless of the language in which they were developed or on which computers they reside. COM-based technologies include ActiveX Controls, Automation, and object linking and embedding (OLE). COM allows an object to expose its functionality to other components and to host applications. It defines both how the object exposes itself and how this exposure works across processes and across networks. COM also defines the object's life cycle.

 COM component
 Binary file - such as .dll, .ocx, and some .exe files - that supports the COM standard for providing objects. Such a file contains code for one or more class factories, COM classes, registry-entry mechanisms, loading code, and so on.

 comparison operator
 An operator that compares two expressions and returns a Boolean value.

 A criteria parameter that may be expressed as ">" (greater than), "\<" (less than), "=" (equal), ">=" (greater than or equal), "<=" (less than or equal), "<>" (not equal), or "like" (pattern matching).

 component
 An object that encapsulates both data and code, and provides a well-specified set of publicly available services.

 compound file
 An implementation of COM structured storage for files. A compound file stores separate objects in a single, structured file consisting of two main elements: storage objects and stream objects. Together, they function like a file system within a file.

 A number of individual files bound together in one physical file. Each individual file in a compound file can be accessed as if it were a single physical file.

 constant
 A numeric or string value that does not change. Named ADO enumerations (enumerated constants) can be used in your code instead of actual values, for example, **adUseClient** is a constant whose value is 3. (Const adUseClient = 3). See also *enumeration*.

 cursor
 A database element that controls record navigation, updateability of data, and the visibility of changes made to the database by other users.

## D
 data binding
 The process of associating the objects or controls of an application to a data source. A control associated with a data source is called a *data-bound control*.

 The contents of a data-bound control are associated with values from a database. For example, a grid control that is bound to a **Recordset** object can be updated when the rows in the **Recordset** are updated. When new values are retrieved by the **Recordset**, new values are displayed in the grid.

 data provider
 Software that exposes data to an ADO application either directly or via a service provider. See also service provider.

 data shaping
 A technique which makes use of a formalized syntax (called **Shape language**) to define a specialized **Recordset** object (called a *shaped Recordset*) that contains not just data, but also references to other **Recordset** objects and/or computed values based on those other **Recordset** objects.

 data source tier
 A logical layer of a distributed system that represents a computer running a DBMS, such as an SQL Server database. See also *client tier*, *middle tier*, *distributed application*.

 DCOM
 A wire protocol that enables COM components to communicate directly with each other across a network. See also *COM*, *component*.

 DDL (Data Definition Language)
 Those statements in SQL that define, as opposed to manipulate, data. The schema of a database is created or modified with DDL. For example, **CREATE TABLE**, **CREATE INDEX**, **GRANT**, and **REVOKE** are SQL DDL statements.

 default stream
 A text or binary stream (represented by a **Stream** object) that is associated with **Record** or **Recordset** objects when using certain OLE DB providers, such as the Microsoft OLE DB Provider for Internet Publishing. The default stream typically contains the contents of a file such as the HTML code for the root of a Web site.

 distributed application
 A program written so that the processing can be divided across multiple computers over a network. Typically, a distributed application is divided into presentation, business logic, and data store layers, or *tiers*. See also client tier, middle tier, data source tier.

 disconnected Recordset
 A **Recordset** object in a client cache that no longer has a live connection to the server. If the original data source needs to be accessed again for some reason, such as updating data, the connection must be re-established. However, the collections, properties, and methods of a disconnected **Recordset** can still be accessed.

 DML (Data Manipulation Language)
 Those statements in SQL that manipulate, as opposed to define, data. The values in a database are selected and modified with DML. For example, **INSERT**, **UPDATE**, **DELETE**, and **SELECT** are SQL DML statements.

 document source provider
 A special class of providers that manage folders and documents. When a document is represented by a **Record** object, or a folder of documents is represented by a **Recordset** object, the document source provider populates those objects with a unique set of fields that describe characteristics of the document, instead of the actual document itself. See also resource record.

 DSN (data source name)
 The collection of information used to connect your application to a particular ODBC database. The ODBC Driver Manager uses this information to create a connection to the database. A DSN can be stored in a file (a file DSN) or in the Windows Registry (a machine DSN).

 dynamic property
 A property specific to a data provider or the cursor service. The **Properties** collection of an object is populated with these automatically ("dynamically"). An object has no dynamic properties until it is connected to a data source through a particular data provider. See also data provider, cursor.

## E
 Enumeration
 A list of named constants. Enumerated values need not be unique. However the name of each value must be unique within the scope where the enumeration is defined. In ADO, enumerations are used for numeric parameter and return values, to add meaning to ADO code and to shield the developer from the numeric values (which may change from version to version). For example, to open a static **Recordset**, use the **adOpenStatic** enumerated value: `Recordset.Open ,,adOpenStatic`

 Also referred to as *enumerated constant*. See also *constant*.

 event
 An action recognized by an object, for which you can write code to respond. Events can be generated by command execution, transaction completion, recordset navigation, and data updates, among other actions. See also *event handler*.

 event handler
 An event handler is the code that is executed when an event occurs. See also event.

## H
 handler
 A routine that manages a common and relatively simple condition or operation, such as error recovery or data management.

 hierarchical Recordset
 A **Recordset** that contains another **Recordset**. See also data shaping, chapter.

 For more information, see [Accessing Rows in a Hierarchical Recordset](../ado/guide/data/accessing-rows-in-a-hierarchical-recordset.md).

 hierarchy
 In general, a hierarchy is a ranked structure with a top level and subordinate levels. In ADO, hierarchical **Recordsets** are used to represent the parent-child relationship between a record and a chapter. Also in ADO, **Record** and **Stream** objects can be used to access hierarchical tree structures such as a folder and documents. ADO MD also includes **Hierarchy** objects to represent a relationship between the levels of a dimension in an OLAP cube. See also hierarchical Recordsets, parent-child relationship, chapter, tree.

## I-L
 ISAPI (Internet Server Application Programming Interface)
 A set of functions for Internet servers, such as a Windows NT® Server/Windows 2000 Server running Microsoft® Internet Information Services (IIS).

 Key
 A column or columns in a table that uniquely identify a row; often used to index a table.

## M
 marshaling
 The process of packaging, sending, and unpackaging interface method parameters across thread or process boundaries.

 middle tier
 The logical layer in a distributed system between a user interface or Web client and the database. This is typically where business objects are instantiated. The middle tier is a collection of business rules and functions that generate and operate upon receiving information. They accomplish this through business rules, which can change frequently, and are thus encapsulated into components that are physically separate from the application logic itself. Also known as *application server tier*. See also distributed application, client tier, data source tier.

 MIME (Multi-purpose Internet Mail Extension)
 An Internet protocol originally developed to allow exchange of electronic mail messages with rich content across heterogeneous network, machine, and e-mail environments. In practice, MIME has also been adopted and extended by non-mail applications.

 MIME is a standard that allows binary data to be published and read on the Internet. The header of a file with binary data contains the MIME type of the data; this informs client programs (Web browsers and mail packages, for instance) that they will need to handle the data in a different way than they handle straight text. For example, the header of a Web document containing a JPEG graphic contains the MIME type specific to the JPEG file format. This allows a browser to display the file with its JPEG viewer, if one is present.

## N-O
 node
 An element in a hierarchical tree structure. A node may be the root, or the child of another node. A node can also be the parent of multiple children. See also hierarchy, tree, root, child, parent.

 object variable
 A variable that contains a reference to an object. For example, `objCustomObject` is a variable that points to an object of type CustomObject:`Set objCustomObject = CreateObject(adodb.Recordset)`

 ODBC (Open Database Connectivity)
 A standard programming language interface used to connect to a variety of data sources. This is usually accessed through Control Panel, where data source names (DSNs) can be assigned to use specific ODBC drivers.

 OLE DB
 A set of interfaces that expose data from a variety of sources using COM. OLE DB interfaces provide applications with uniform access to data stored in diverse information sources. These interfaces support the amount of DBMS functionality appropriate to the data source, enabling it to share its data. See also COM.

 optimistic locking
 A type of locking in which the data page containing one or more records, including the record being edited, is unavailable to other users only while the record is being updated by the **Update** method, but is available before and after the call to **Update**.

 Optimistic locking is used when the **Recordset** object is opened with the **LockType** parameter or property set to **adLockOptimistic** or **adLockBatchOptimistic**. See also pessimistic locking.

 ordinal value
 The numeric location of an item within an order. In an ADO collection, the ordinal value of the first item is zero (0). The next item is one (1), and so on.

## P
 parameterized command
 A query or command that allows you to set parameter values before the command is executed. For example, a SQL string can be parameterized by embedding parameter markers in the SQL string (designated by the '?' character). The application then specifies values for each parameter and executes the command.

 parent
 The controlling side of a hierarchical relationship. In a hierarchical structure, a parent has one or more child nodes directly beneath it in the hierarchy. See also parent-alias, parent-child relationship, child.

 parent-alias
 An alias that refers to the parent. See also alias, parent.

 parent-child relationship
 A relationship in a hierarchical structure in which the parent is one level higher and directly associated with one or more children. A child is one level lower and must have one parent. See also parent, child.

 pessimistic locking
 A type of locking in which the page containing one or more records, including the record being edited, is unavailable to other users to ensure that an update will be made. Pessimistic locking behavior is defined by the OLE DB provider. Typically, records are locked upon editing and remain unavailable until the **Update** method has completed.

 Pessimistic locking is enabled when the **Recordset** object is opened with the **LockType** parameter or property set to **adLockPessimistic**. See also optimistic locking.

 pooling
 A performance optimization based on using collections of pre-allocated resources, such as objects or database connections. It is more efficient to draw an existing resource from the pool than to create a new resource.

 ProgID (programmatic identifier)
 A unique name mapped to the Windows registry by a COM application. The ProgID for an ADO Connection is "ADODB.Connection". See also CLSID, COM.

 proxy
 An interface-specific object that provides the parameter marshaling and communication required for a client to call an application object that is running in a different execution environment, such as on a different thread or in another process. The proxy is located with the client and communicates with a corresponding stub that is located with the application object that is being called. See also stub.

## R
 relative URL
 A partially qualified URL that specifies a resource on the Internet or an intranet whose location is relative to a starting point specified by an absolute URL or equivalent ADO Connection object. In effect, the concatenated absolute and relative URLs consitute a complete URL. See also URL and absolute URL.

 remote data source
 A data source that exists on a another computer, rather than on the local system (where the client application runs).

 resource record
 A record from a document source provider that contains fields for the definition and description of a folder or document. The document itself is not contained in the resource record but typically can be accessed by the default stream or a field in the resource record containing a URL. See also document source provider, default stream, URL.

 rowset
 A set of rows from a data source, all having the same field schema. A rowset can represent all or some fields from a table. A rowset can also represent a virtual table, created by a query or a join of two or more tables. In ADO, rowsets are represented by **Recordset** objects.

## S
 Scope
 The range of reference for an object or variable or a range of records in a view or table. For example, local variables can be referenced only within the procedure in which they were defined. Public variables are accessible from anywhere in the application. Objects, such as the current database, are in scope if they are in the defined search path. Record ranges can be specified with a Scope clause in many commands.

 service provider
 Software that encapsulates a service by producing and consuming data, augmenting features in your ADO applications. It is a provider that does not directly expose data, rather it provides a service, such as query processing. The service provider may process data provided by a data provider. See also data provider.

 shaped Recordset
 A **Recordset** whose columns have been specifically defined to contain not just data, but also references (called chapters) to other **Recordset** objects and/or computed values based on other **Recordset** objects.

 sibling
 Any two or more nodes in a hierarchical structure that are on the same level in the hierarchy. The root node in a hierarchy has no siblings.

 stored procedure
 A precompiled collection of code such as SQL statements and optional control-of-flow statements stored under a name and processed as a unit. Stored procedures are stored within a database; they can be executed with one call from an application and allow user-declared variables, conditional execution, and other powerful programming features.

 stub
 An interface-specific object that provides the parameter marshaling and communication required for an application object to receive calls from a client that is running in a different execution environment, such as on a different thread or in another process. The stub is located with the application object and communicates with a corresponding proxy that is located with the client that calls it. See also proxy.

 sub-node
 See child.

 synchronous operation
 An operation initiated by code that completes before the next operation may start. See also asynchronous operation.

## T-Z
 Tree
 A structure representing a hierarchical relationship between elements (nodes). There is one node at the top level of a tree (the root). Underneath the root, there can be multiple children. Each child may in turn be the parent of other children, thus branching like a tree. A folder containing documents and other folders is a typical example of a tree structure. See also hierarchy, node, root, child, parent.

 Web server
 A computer that provides Web services and pages to intranet and Internet users.
