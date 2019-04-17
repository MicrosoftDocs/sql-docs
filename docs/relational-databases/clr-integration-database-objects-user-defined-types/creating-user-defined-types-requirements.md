---
title: "User-Defined Type Requirements | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "UDTs [CLR integration], requirements"
  - "serialization"
  - "Native serialization format [CLR integration]"
  - "attributes [CLR integration]"
  - "XML serialization [CLR integration]"
  - "user-defined types [CLR integration], requirements"
  - "user-defined serialization [CLR integration]"
  - "user-defined types [CLR integration], Native serialization"
  - "UDTs [CLR integration], Native serialization"
ms.assetid: bedc3372-50eb-40f2-bcf2-d6db6a63b7e6
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# Creating User-Defined Types - Requirements
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  You must make several important design decisions when creating a user-defined type (UDT) to be installed in [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For most UDTs, creating the UDT as a structure is recommended, although creating it as a class is also an option. The UDT definition must conform to the specifications for creating UDTs in order for it to be registered with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Requirements for Implementing UDTs  
 To run in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], your UDT must implement the following requirements in the UDT definition:  
  
 The UDT must specify the **Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute**. The use of the **System.SerializableAttribute** is optional, but recommended.  
  
-   The UDT must implement the **System.Data.SqlTypes.INullable** interface in the class or structure by creating a public **static** (**Shared** in [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Basic) **Null** method. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is null-aware by default. This is necessary for code executing in the UDT to be able to recognize a null value.  
  
-   The UDT must contain a public **static** (or **Shared**) **Parse** method that supports parsing from, and a public **ToString** method for converting to a string representation of the object.  
  
-   A UDT with a user-defined serialization format must implement the **System.Data.IBinarySerialize** interface and provide a **Read** and a **Write** method.  
  
-   The UDT must implement **System.Xml.Serialization.IXmlSerializable**, or all public fields and properties must be of types that are XML serializable or decorated with the **XmlIgnore** attribute if overriding standard serialization is required.  
  
-   There must be only one serialization of a UDT object. Validation fails if the serialize or deserialize routines recognize more than one representation of a particular object.  
  
-   **SqlUserDefinedTypeAttribute.IsByteOrdered** must be **true** to compare data in byte order. If the IComparable interface is not implemented and **SqlUserDefinedTypeAttribute.IsByteOrdered** is **false**, byte order comparisons will fail.  
  
-   A UDT defined in a class must have a public constructor that takes no arguments. You can optionally create additional overloaded class constructors.  
  
-   The UDT must expose data elements as public fields or property procedures.  
  
-   Public names cannot be longer than 128 characters, and must conform to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] naming rules for identifiers as defined in [Database Identifiers](../../relational-databases/databases/database-identifiers.md).  
  
-   **sql_variant** columns cannot contain instances of a UDT.  
  
-   Inherited members are not accessible from [!INCLUDE[tsql](../../includes/tsql-md.md)] because the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type system is not aware of the inheritance hierarchy among UDTs. However, you can use inheritance when you structure your classes and you can call such methods in the managed code implementation of the type.  
  
-   Members cannot be overloaded, except for the class constructor. If you do create an overloaded method, no error is raised when you register the assembly or create the type in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Detection of the overloaded method occurs at run time, not when the type is created. Overloaded methods can exist in the class as long as they are never invoked. Once you invoke the overloaded method, an error is raised.  
  
-   Any **static** (or **Shared**) members must be declared as constants or as read-only. Static members cannot be mutable.  
  
-   If the **SqlUserDefinedTypeAttribute.MaxByteSize** field is set to -1, the serialized UDT can be as large as the large object (LOB) size limit (currently 2 GB). The size of the UDT cannot exceed the value specified in the **MaxByteSized** field.  
  
> [!NOTE]  
>  Although it is not used by the server for performing comparisons, you can optionally implement the **System.IComparable** interface, which exposes a single method, **CompareTo**. This is used on the client side in situations in which it is desirable to accurately compare or order UDT values.  
  
## Native Serialization  
 Choosing the right serialization attributes for your UDT depends on the type of UDT you are trying to create. The **Native** serialization format utilizes a very simple structure that enables [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to store an efficient native representation of the UDT on disk. The **Native** format is recommended if the UDT is simple and only contains fields of the following types:  
  
 **bool**, **byte**, **sbyte**, **short**, **ushort**, **int**, **uint**, **long**, **ulong**, **float**, **double**, **SqlByte**, **SqlInt16**, **SqlInt32**, **SqlInt64**, **SqlDateTime**, **SqlSingle**, **SqlDouble**, **SqlMoney**, **SqlBoolean**  
  
 Value types that are composed of fields of the above types are good candidates for **Native** format, such as **structs** in Visual C#, (or **Structures** as they are known in Visual Basic). For example, a UDT specified with the **Native** serialization format may contain a field of another UDT that was also specified with the **Native** format. If the UDT definition is more complex and contains data types not on the above list, you must specify the **UserDefined** serialization format instead.  
  
 The **Native** format has the following requirements:  
  
-   The type must not specify a value for **Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute.MaxByteSize**.  
  
-   All fields must be serializable.  
  
-   The **System.Runtime.InteropServices.StructLayoutAttribute** must be specified as **StructLayout.LayoutKindSequential** if the UDT is defined in a class and not a structure. This attribute controls the physical layout of the data fields and is used to force the members to be laid out in the order in which they appear. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses this attribute to determine the field order for UDTs with multiple values.  
  
 For an example of a UDT defined with **Native** serialization, see the Point UDT in [Coding User-Defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/creating-user-defined-types-coding.md).  
  
## UserDefined Serialization  
 The **UserDefined** format setting for the **Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute** attribute gives the developer full control over the binary format. When specifying the **Format** attribute property as **UserDefined**, you must do the following in your code:  
  
-   Specify the optional **IsByteOrdered** attribute property. The default value is **false**.  
  
-   Specify the **MaxByteSize** property of the **Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute**.  
  
-   Write code to implement **Read** and **Write** methods for the UDT by implementing the **System.Data.Sql.IBinarySerialize** interface.  
  
 For an example of a UDT defined with **UserDefined** serialization, see the Currency UDT in [Coding User-Defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/creating-user-defined-types-coding.md).  
  
> [!NOTE]  
>  UDT fields must use native serialization or be persisted in order to be indexed.  
  
## Serialization Attributes  
 Attributes determine how serialization is used to construct the storage representation of UDTs and to transmit UDTs by value to the client. You are required to specify the **Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute** when creating the UDT. The **Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute** attribute indicates that the class is a UDT and specifies the storage for the UDT. You can optionally specify the **Serializable** attribute, although [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not require this.  
  
 The **Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute** has the following properties.  
  
 **Format**  
 Specifies the serialization format, which can be **Native** or **UserDefined**, depending on the data types of the UDT.  
  
 **IsByteOrdered**  
 A **Boolean** value that determines how [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] performs binary comparisons on the UDT.  
  
 **IsFixedLength**  
 Indicates whether all instances of this UDT are the same length.  
  
 **MaxByteSize**  
 The maximum size of the instance, in bytes. You must specify **MaxByteSize** with the **UserDefined** serialization format. For a UDT with user-defined serialization specified, **MaxByteSize** refers to the total size of the UDT in its serialized form as defined by the user. The value of **MaxByteSize** must be in the range of 1 to 8000, or set to -1 to indicate that the UDT is greater than 8000 bytes (the total size cannot exceed the maximum LOB size). Consider a UDT with a property of a string of 10 characters (**System.Char**). When the UDT is serialized by using a BinaryWriter, the total size of the serialized string is 22 bytes: 2 bytes per Unicode UTF-16 character, multiplied by the maximum number of characters, plus 2 control bytes of overhead incurred from serializing a binary stream. Therefore, when determining the value of **MaxByteSize**, the total size of the serialized UDT must be considered: the size of the data serialized in binary form plus the overhead incurred by serialization.  
  
 **ValidationMethodName**  
 The name of the method used to validate instances of the UDT.  
  
### Setting IsByteOrdered  
 When the **Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute.IsByteOrdered** property is set to **true**, you are in effect guaranteeing that the serialized binary data can be used for semantic ordering of the information. Thus, each instance of a byte-ordered UDT object can only have one serialized representation. When a comparison operation is performed in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on the serialized bytes, its results should be the same as if the same comparison operation had taken place in managed code. The following features are also supported when **IsByteOrdered** is set to **true**:  
  
-   The ability to create indexes on columns of this type.  
  
-   The ability to create primary and foreign keys as well as CHECK and UNIQUE constraints on columns of this type.  
  
-   The ability to use [!INCLUDE[tsql](../../includes/tsql-md.md)] ORDER BY, GROUP BY, and PARTITION BY clauses. In these cases, the binary representation of the type is used to determine the order.  
  
-   The ability to use comparison operators in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements.  
  
-   The ability to persist computed columns of this type.  
  
 Note that both the **Native** and **UserDefined** serialization formats support the following comparison operators when **IsByteOrdered** is set to **true**:  
  
-   Equal to (=)  
  
-   Not equal to (!=)  
  
-   Greater than (>)  
  
-   Less than (\<)  
  
-   Greater than or equal to (>=)  
  
-   Less than or equal to (<=)  
  
### Implementing Nullability  
 In addition to specifying the attributes for your assemblies correctly, your class must also support nullability. UDTs loaded into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are null-aware, but in order for the UDT to recognize a null value, the class must implement the **INullable** interface. For more information and an example of how to implement nullability in a UDT, see [Coding User-Defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/creating-user-defined-types-coding.md).  
  
### String Conversions  
 To support string conversion to and from the UDT, you must provide a **Parse** method and a **ToString** method in your class. The **Parse** method allows a string to be converted into a UDT. It must be declared as **static** (or **Shared** in Visual Basic), and take a parameter of type **System.Data.SqlTypes.SqlString**. For more information and an example of how to implement the **Parse** and **ToString** methods, see [Coding User-Defined Types](../../relational-databases/clr-integration-database-objects-user-defined-types/creating-user-defined-types-coding.md).  
  
## XML Serialization  
 UDTs must support conversion to and from the **xml** data type by conforming to the contract for XML serialization. The **System.Xml.Serialization** namespace contains classes that are used to serialize objects into XML format documents or streams. You can choose to implement **xml** serialization by using the **IXmlSerializable** interface, which provides custom formatting for XML serialization and deserialization.  
  
 In addition to performing explicit conversions from UDT to **xml**, XML serialization enables you to:  
  
-   Use **Xquery** over values of UDT instances after conversion to the **xml** data type.  
  
-   Use UDTs in parameterized queries and Web methods with Native XML Web Services in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Use UDTs to receive a bulk load of XML data.  
  
-   Serialize DataSets that contain tables with UDT columns.  
  
 UDTs are not serialized in FOR XML queries. To execute a FOR XML query that displays the XML serialization of UDTs, explicitly convert each UDT column to the **xml** data type in the SELECT statement. You can also explicitly convert the columns to **varbinary**, **varchar**, or **nvarchar**.  
  
## See Also  
 [Creating a User-Defined Type](../../relational-databases/clr-integration-database-objects-user-defined-types/creating-user-defined-types.md)  
  
  
