---
title: "Using Visual C++ Extensions"
description: "Using Visual C++ Extensions"
author: rothja
ms.author: jroth
ms.date: 11/08/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "Visual C++ [ADO], using VC++ extensions"
  - "ADO, Visual C++"
dev_langs:
  - "C++"
---
# Visual C++ Extensions
## The IADORecordBinding Interface
 The Microsoft Visual C++ Extensions for ADO associate, or bind, fields of a [Recordset](../../reference/ado-api/recordset-object-ado.md) object to C/C++ variables. Whenever the current row of the bound **Recordset** changes, all the bound fields in the **Recordset** are copied to the C/C++ variables. If necessary, the copied data is converted to the declared data type of the C/C++ variable.

 The **BindToRecordset** method of the **IADORecordBinding** interface binds fields to C/C++ variables. The **AddNew** method adds a new row to the bound **Recordset**. The **Update** method populates fields in new rows of the **Recordset**, or updates fields in existing rows, with the value of the C/C++ variables.

 The **IADORecordBinding** interface is implemented by the **Recordset** object. You do not code the implementation yourself.

## Binding Entries
 The Visual C++ Extensions for ADO map fields of a [Recordset](../../reference/ado-api/recordset-object-ado.md) object to C/C++ variables. The definition of a mapping between a field and a variable is called a *binding entry*. Macros provide binding entries for numeric, fixed-length, and variable-length data. The binding entries and C/C++ variables are declared in a class derived from the Visual C++ Extensions class, **CADORecordBinding**. The **CADORecordBinding** class is defined internally by the binding entry macros.

 ADO internally maps the parameters in these macros to an OLE DB **DBBINDING** structure and creates an OLE DB **Accessor** object to manage the movement and conversion of data between fields and variables. OLE DB defines data as consisting of three parts: A *buffer* where the data is stored; a *status* that indicates whether a field was successfully stored in the buffer, or how the variable should be restored to the field; and the *length* of the data. (See [Getting and Setting Data (OLE DB)](/previous-versions/windows/desktop/ms713700(v=vs.85))in the OLE DB Programmer's Reference, for more information.)

## Header File
 Include the following file in your application in order to use the Visual C++ Extensions for ADO:

```cpp
#include <icrsint.h>
```

## Binding Recordset Fields

#### To Bind Recordset Fields to C/C++ Variables

1.  Create a class derived from the **CADORecordBinding** class.

2.  Specify binding entries and corresponding C/C++ variables in the derived class. Bracket the binding entries between **BEGIN_ADO_BINDING** and **END_ADO_BINDING** macros. Do not terminate the macros with commas or semicolons. Appropriate delimiters are specified automatically by each macro.

     Specify one binding entry for each field to be mapped to a C/C++ variable. Use an appropriate member from the **ADO_FIXED_LENGTH_ENTRY**, **ADO_NUMERIC_ENTRY**, or **ADO_VARIABLE_LENGTH_ENTRY** family of macros.

3.  In your application, create an instance of the class derived from **CADORecordBinding**. Get the **IADORecordBinding** interface from the **Recordset**. Then call the **BindToRecordset** method to bind the **Recordset** fields to the C/C++ variables.

 For more information, see the [Visual C++ Extensions Example](./visual-c-extensions-example.md).

## Interface Methods
 The **IADORecordBinding** interface has three methods: **BindToRecordset**, **AddNew**, and **Update**. The sole argument to each method is a pointer to an instance of the class derived from **CADORecordBinding**. Therefore, the **AddNew** and **Update** methods cannot specify any of the parameters of their ADO method namesakes.

## Syntax
 The **BindToRecordset** method associates the **Recordset** fields with C/C++ variables.

```cpp
BindToRecordset(CADORecordBinding *binding)
```

 The **AddNew** method invokes its namesake, the ADO [AddNew](../../reference/ado-api/addnew-method-ado.md) method, to add a new row to the **Recordset**.

```cpp
AddNew(CADORecordBinding *binding)
```

 The **Update** method invokes its namesake, the ADO [Update](../../reference/ado-api/update-method.md) method, to update the **Recordset**.

```cpp
Update(CADORecordBinding *binding)
```

## Binding Entry Macros
 Binding entry macros define the association of a **Recordset** field and a variable. A beginning and ending macro delimits the set of binding entries.

 Families of macros are provided for fixed-length data, such as **adDate** or **adBoolean**; numeric data, such as **adTinyInt**, **adInteger**, or **adDouble**; and variable-length data, such as **adChar**, **adVarChar** or **adVarBinary**. All numeric types, except for **adVarNumeric**, are also fixed-length types. Each family has differing sets of parameters so that you can exclude binding information that is of no interest.

 For more information, see [Appendix A: Data Types](/previous-versions/windows/desktop/ms723969(v=vs.85)), of the OLE DB Programmer's Reference.

### Begin Binding Entries
 **BEGIN_ADO_BINDING**(*Class*)

### Fixed-Length Data
 **ADO_FIXED_LENGTH_ENTRY**(*Ordinal, DataType, Buffer, Status, Modify*)

 **ADO_FIXED_LENGTH_ENTRY2**(*Ordinal, DataType, Buffer, Modify*)

### Numeric Data
 **ADO_NUMERIC_ENTRY**(*Ordinal, DataType, Buffer, Precision, Scale, Status, Modify*)

 **ADO_NUMERIC_ENTRY2**(*Ordinal, DataType, Buffer, Precision, Scale, Modify*)

### Variable-Length Data
 **ADO_VARIABLE_LENGTH_ENTRY**(*Ordinal, DataType, Buffer, Size, Status, Length, Modify*)

 **ADO_VARIABLE_LENGTH_ENTRY2**(*Ordinal, DataType, Buffer, Size, Status, Modify*)

 **ADO_VARIABLE_LENGTH_ENTRY3**(*Ordinal, DataType, Buffer, Size, Length, Modify*)

 **ADO_VARIABLE_LENGTH_ENTRY4**(*Ordinal, DataType, Buffer, Size, Modify*)

### End Binding Entries
 **END_ADO_BINDING**()

|Parameter|Description|
|---------------|-----------------|
|*Class*|Class in which the binding entries and C/C++ variables are defined.|
|*Ordinal*|Ordinal number, counting from one, of the **Recordset** field corresponding to your C/C++ variable.|
|*DataType*|Equivalent ADO data type of the C/C++ variable (see [DataTypeEnum](../../reference/ado-api/datatypeenum.md) for a list of valid data types). The value of the **Recordset** field will be converted to this data type if necessary.|
|*Buffer*|Name of the C/C++ variable where the **Recordset** field will be stored.|
|*Size*|Maximum size in bytes of *Buffer*. If *Buffer* will contain a variable-length string, allow room for a terminating zero.|
|*Status*|Name of a variable that will indicate whether the contents of *Buffer* are valid, and whether the conversion of the field to *DataType* was successful.<br /><br /> The two most important values for this variable are **adFldOK**, which means the conversion was successful; and **adFldNull**, which means the value of the field would be a VARIANT of type VT_NULL and not merely empty.<br /><br /> Possible values for *Status* are listed in the next table, "Status Values."|
|*Modify*|Boolean flag; if TRUE, indicates ADO is allowed to update the corresponding **Recordset** field with the value contained in *Buffer*.<br /><br /> Set the Boolean *modify* parameter to TRUE to enable ADO to update the bound field, and FALSE if you want to examine the field but not change it.|
|*Precision*|Number of digits that can be represented in a numeric variable.|
|*Scale*|Number of decimal places in a numeric variable.|
|*Length*|Name of a four-byte variable that will contain the actual length of the data in *Buffer*.|

## Status Values
 The value of the *Status* variable indicates whether a field was successfully copied to a variable.

 When setting data, *Status* may be set to **adFldNull** to indicate the **Recordset** field should be set to null.

|Constant|Value|Description|
|--------------|-----------|-----------------|
|**adFldOK**|0|A non-null field value was returned.|
|**adFldBadAccessor**|1|Binding was invalid.|
|**adFldCantConvertValue**|2|Value couldn't be converted for reasons other than sign mismatch or data overflow.|
|**adFldNull**|3|When getting a field, indicates a null value was returned.<br /><br /> When setting a field, indicates the field should be set to **NULL** when the field cannot encode **NULL** itself (for example, a character array or an integer).|
|**adFldTruncated**|4|Variable-length data or numeric digits were truncated.|
|**adFldSignMismatch**|5|Value is signed and variable data type is unsigned.|
|**adFldDataOverFlow**|6|Value is larger than could be stored in the variable data type.|
|**adFldCantCreate**|7|Unknown column type and field already open.|
|**adFldUnavailable**|8|Field value could not be determined-for example, on a new, unassigned field with no default value.|
|**adFldPermissionDenied**|9|When updating, no permission to write data.|
|**adFldIntegrityViolation**|10|When updating, field value would violate column integrity.|
|**adFldSchemaViolation**|11|When updating, field value would violate column schema.|
|**adFldBadStatus**|12|When updating, invalid status parameter.|
|**adFldDefault**|13|When updating, a default value was used.|

## See Also
 [Visual C++ Extensions Example](./visual-c-extensions-example.md)
 [Visual C++ Extensions Header](./visual-c-extensions-header.md)