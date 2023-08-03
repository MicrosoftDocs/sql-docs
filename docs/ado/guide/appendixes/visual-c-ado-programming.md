---
title: "Visual C++ ADO Programming"
description: "Visual C++ ADO Programming"
author: rothja
ms.author: jroth
ms.date: 11/08/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "ADO, Visual C++"
  - "Visual C++ [ADO]"
dev_langs:
  - "C++"
---
# Visual C++ ADO Programming
The ADO API Reference describes the functionality of the ADO application programming interface (API) using a syntax similar to Microsoft Visual Basic. Though the intended audience is all users, ADO programmers employ diverse languages such as Visual Basic, Visual C++ (with and without the **#import** directive), and Visual J++ (with the ADO/WFC class package).  

> [!NOTE]
> Microsoft ended support for Visual J++ in 2004.

 To accommodate this diversity, the [ADO for Visual C++ Syntax Indexes](./using-ado-with-microsoft-visual-c.md) provide Visual C++ language-specific syntax with links to common descriptions of functionality, parameters, exceptional behaviors, and so on, in the API Reference.  
  
 ADO is implemented with COM (Component Object Model) interfaces. However, it is easier for programmers to work with COM in certain programming languages than others. For example, nearly all the details of using COM are handled implicitly for Visual Basic programmers, whereas Visual C++ programmers must attend to those details themselves.  
  
 The following sections summarize details for C and C++ programmers using ADO and the **#import** directive. It focuses on data types specific to COM (**Variant**, **BSTR**, and **SafeArray**), and error handling (_com_error).  
  
## Using the #import Compiler Directive  
 The **#import** Visual C++ compiler directive simplifies working with the ADO methods and properties. The directive takes the name of a file containing a type library, such as the ADO .dll (Msado15.dll), and generates header files containing typedef declarations, smart pointers for interfaces, and enumerated constants. Each interface is encapsulated, or wrapped, in a class.  
  
 For each operation within a class (that is, a method or property call), there is a declaration to call the operation directly (that is, the "raw" form of the operation), and a declaration to call the raw operation and throw a COM error if the operation fails to execute successfully. If the operation is a property, there is usually a compiler directive that creates an alternative syntax for the operation that has syntax like Visual Basic.  
  
 Operations that retrieve the value of a property have names of the form, **Get**_Property_. Operations that set the value of a property have names of the form, **Put**_Property_. Operations that set the value of a property with a pointer to an ADO object have names of the form, **PutRef**_Property_.  
  
 You can get or set a property with calls of these forms:  
  
```cpp
variable = objectPtr->GetProperty(); // get property value   
objectPtr->PutProperty(value);       // set property value  
objectPtr->PutRefProperty(&value);   // set property with object pointer  
```
  
## Using Property Directives  
 The **__declspec(property...)** compiler directive is a Microsoft-specific C language extension that declares a function used as a property to have an alternative syntax. As a result, you can set or get values of a property in a way similar to Visual Basic. For example, you can set and get a property this way:  
  
```cpp
objectPtr->property = value;        // set property value  
variable = objectPtr->property;     // get property value  
```
  
 Notice you do not have to code:  
  
```cpp
objectPtr->PutProperty(value);      // set property value  
variable = objectPtr->GetProperty;  // get property value  
```
  
 The compiler will generate the appropriate **Get**_-_, **Put**-, or **PutRef**_Property_ call based on what alternative syntax is declared and whether the property is being read or written.  
  
 The **__declspec(property...)** compiler directive can only declare **get**, **put**, or **get** and **put** alternative syntax for a function. Read-only operations only have a **get** declaration; write-only operations only have a **put** declaration; operations that are both read and write have both **get** and **put** declarations.  
  
 Only two declarations are possible with this directive; however, each property may have three property functions: **Get**_Property_, **Put**_Property_, and **PutRef**_Property_. In that case, only two forms of the property have the alternative syntax.  
  
 For example, the **Command** object **ActiveConnection** property is declared with an alternative syntax for **Get**_ActiveConnection_ and **PutRef**_ActiveConnection_. The **PutRef**- syntax is a good choice because in practice, you will typically want to put an open **Connection** object (that is, a **Connection** object pointer) in this property. On the other hand, the **Recordset** object has **Get**-, **Put**-, and **PutRef**_ActiveConnection_ operations, but no alternative syntax.  
  
## Collections, the GetItem Method, and the Item Property  

 ADO defines several collections, including **Fields**, **Parameters**, **Properties**, and **Errors**. In Visual C++, the **GetItem(_index_)** method returns a member of the collection. *Index* is a **Variant**, the value of which is either a numeric index of the member in the collection, or a string containing the name of the member.  
  
 The **__declspec(property...)** compiler directive declares the **Item** property as an alternative syntax to each collection's fundamental **GetItem()** method. The alternative syntax uses square brackets and looks similar to an array reference. In general, the two forms look like the following:  
  
```cpp
  
      collectionPtr->GetItem(index);  
collectionPtr->Item[index];  
```
  
 For example, assign a value to a field of a **Recordset** object, named **_rs_**, derived from the **authors** table of the **pubs** database. Use the **Item()** property to access the third **Field** of the **Recordset** object **Fields** collection (collections are indexed from zero; assume the third field is named **_au\_fname_**). Then call the **Value()** method on the **Field** object to assign a string value.  
  
 This can be expressed in Visual Basic in the following four ways (the last two forms are unique to Visual Basic; other languages do not have equivalents):  
  
```cpp
rs.Fields.Item(2).Value = "value"  
rs.Fields.Item("au_fname").Value = "value"  
rs(2) = "value"  
rs!au_fname = "value"  
```
  
 The equivalent in Visual C++ to the first two forms above is:  
  
```cpp
rs->Fields->GetItem(long(2))->PutValue("value");   
rs->Fields->GetItem("au_fname")->PutValue("value");  
```
  
 -or- (the alternative syntax for the **Value** property is also shown)  
  
```cpp
rs->Fields->Item[long(2)]->Value = "value";  
rs->Fields->Item["au_fname"]->Value = "value";  
```
  
 For examples of iterating through a collection, see the "ADO Collections" section of "ADO Reference".  
  
## COM-Specific Data Types  
 In general, any Visual Basic data type you find in the ADO API Reference has a Visual C++ equivalent. These include standard data types such as **unsigned char** for a Visual Basic **Byte**, **short** for **Integer**, and **long** for **Long**. Look in the Syntax Indexesto see exactly what is required for the operands of a given method or property.  
  
 The exceptions to this rule are the data types specific to COM: **Variant**, **BSTR**, and **SafeArray**.  
  
### Variant  
 A **Variant** is a structured data type that contains a value member and a data type member. A **Variant** may contain a wide range of other data types including another Variant, BSTR, Boolean, IDispatch or IUnknown pointer, currency, date, and so on. COM also provides methods that make it easy to convert one data type to another.  
  
 The **_variant_t** class encapsulates and manages the **Variant** data type.  
  
 When the ADO API Reference says a method or property operand takes a value, it usually means the value is passed in a **_variant_t**.  
  
 This rule is explicitly true when the **Parameters** section in the topics of the ADO API Reference says an operand is a **Variant**. One exception is when the documentation explicitly says the operand takes a standard data type, such as **Long** or **Byte**, or an enumeration. Another exception is when the operand takes a **String**.  
  
### BSTR  
 A **BSTR** (**B**asic **STR**ing) is a structured data type that contains a character string and the string's length. COM provides methods to allocate, manipulate, and free a **BSTR**.  
  
 The **_bstr_t** class encapsulates and manages the **BSTR** data type.  
  
 When the ADO API Reference says a method or property takes a **String** value, it means the value is in the form of a **_bstr_t**.  
  
### Casting _variant_t and _bstr_t Classes  
 Often it is not necessary to explicitly code a **_variant_t** or **_bstr_t** in an argument to an operation. If the **_variant_t** or **_bstr_t** class has a constructor that matches the data type of the argument, the compiler will generate the appropriate **_variant_t** or **_bstr_t**.  
  
 However, if the argument is ambiguous, that is, the argument's data type matches more than one constructor, you must cast the argument with the appropriate data type to invoke the correct constructor.  
  
 For example, the declaration for the **Recordset::Open** method is:  
  
```cpp
    HRESULT Open (  
        const _variant_t & Source,  
        const _variant_t & ActiveConnection,  
        enum CursorTypeEnum CursorType,  
        enum LockTypeEnum LockType,  
        long Options );  
```
  
 The `ActiveConnection` argument takes a reference to a **_variant_t**, which you may code as a connection string or a pointer to an open **Connection** object.  
  
 The correct **_variant_t** will be constructed implicitly if you pass a string such as "`DSN=pubs;uid=MyUserName;pwd=MyPassword;`", or a pointer such as "`(IDispatch *) pConn`".  
  
> [!NOTE]
>  If you are connecting to a data source provider that supports Windows authentication, you should specify **Trusted_Connection=yes** or **Integrated Security = SSPI** instead of user ID and password information in the connection string.  
  
 Or you may explicitly code a **_variant_t** containing a pointer such as "`_variant_t((IDispatch *) pConn, true)`". The cast, `(IDispatch *)`, resolves the ambiguity with another constructor that takes a pointer to an IUnknown interface.  
  
 It is a crucial, though seldom mentioned fact, that ADO is an IDispatch interface. Whenever a pointer to an ADO object must be passed as a **Variant**, that pointer must be cast as a pointer to an IDispatch interface.  
  
 The last case explicitly codes the second boolean argument of the constructor with its optional, default value of `true`. This argument causes the **Variant** constructor to call its **AddRef**() method, which compensates for ADO automatically calling the **_variant_t::Release**() method when the ADO method or property call completes.  
  
### SafeArray  
 A **SafeArray** is a structured data type that contains an array of other data types. A **SafeArray** is called *safe* because it contains information about the bounds of each array dimension, and limits access to array elements within those bounds.  
  
 When the ADO API Reference says a method or property takes or returns an array, it means the method or property takes or returns a **SafeArray**, not a native C/C++ array.  
  
 For example, the second parameter of the **Connection** object **OpenSchema** method requires an array of **Variant** values. Those **Variant** values must be passed as elements of a **SafeArray**, and that **SafeArray** must be set as the value of another **Variant**. It is that other **Variant** that is passed as the second argument of **OpenSchema**.  
  
 As further examples, the first argument of the **Find** method is a **Variant** whose value is a one-dimensional **SafeArray**; each of the optional first and second arguments of **AddNew** is a one-dimensional **SafeArray**; and the return value of the **GetRows** method is a **Variant** whose value is a two-dimensional **SafeArray**.  
  
## Missing and Default Parameters  
 Visual Basic allows missing parameters in methods. For example, the **Recordset** object **Open** method has five parameters, but you can skip intermediate parameters and leave off trailing parameters. A default **BSTR** or **Variant** will be substituted depending on the data type of the missing operand.  
  
 In C/C++, all operands must be specified. If you want to specify a missing parameter whose data type is a string, specify a **_bstr_t** containing a null string. If you want to specify a missing parameter whose data type is a **Variant**, specify a **_variant_t** with a value of DISP_E_PARAMNOTFOUND and a type of VT_ERROR. Alternatively, specify the equivalent **_variant_t** constant, **vtMissing**, which is supplied by the **#import** directive.  
  
 Three methods are exceptions to the typical use of **vtMissing**. These are the **Execute** methods of the **Connection** and **Command** objects, and the **NextRecordset** method of the **Recordset** object. The following are their signatures:  
  
```cpp
_RecordsetPtr <A HREF="mdmthcnnexecute.htm">Execute</A>( _bstr_t CommandText, VARIANT * RecordsAffected,   
        long Options );  // Connection  
_RecordsetPtr <A HREF="mdmthcmdexecute.htm">Execute</A>( VARIANT * RecordsAffected, VARIANT * Parameters,   
        long Options );  // Command  
_RecordsetPtr <A HREF="mdmthnextrec.htm">NextRecordset</A>( VARIANT * RecordsAffected );  // Recordset  
```
  
 The parameters, *RecordsAffected* and *Parameters*, are pointers to a **Variant**. *Parameters* is an input parameter which specifies the address of a **Variant** containing a single parameter, or array of parameters, that will modify the command being executed. *RecordsAffected* is an output parameter that specifies the address of a **Variant**, where the number of rows affected by the method is returned.  
  
 In the **Command** object **Execute** method, indicate that no parameters are specified by setting *Parameters* to either `&vtMissing` (which is recommended) or to the null pointer (that is, **NULL** or zero (0)). If *Parameters* is set to the null pointer, the method internally substitutes the equivalent of **vtMissing**, and then completes the operation.  
  
 In all the methods, indicate that the number of records affected should not be returned by setting *RecordsAffected* to the null pointer. In this case, the null pointer is not so much a missing parameter as an indication that the method should discard the number of records affected.  
  
 Thus, for these three methods, it is valid to code something such as:  
  
```cpp
pConnection->Execute("commandText", NULL, adCmdText);   
pCommand->Execute(NULL, NULL, adCmdText);  
pRecordset->NextRecordset(NULL);  
```
  
## Error Handling  
 In COM, most operations return an HRESULT return code that indicates whether a function completed successfully. The **#import** directive generates wrapper code around each "raw" method or property and checks the returned HRESULT. If the HRESULT indicates failure, the wrapper code throws a COM error by calling _com_issue_errorex() with the HRESULT return code as an argument. COM error objects can be caught in a **try**-**catch** block. (For efficiency's sake, catch a reference to a **_com_error** object.)  
  
 Remember, these are ADO errors: they result from the ADO operation failing. Errors returned by the underlying provider appear as **Error** objects in the **Connection** object **Errors** collection.  
  
 The **#import** directive creates only error handling routines for methods and properties declared in the ADO .dll. However, you can take advantage of this same error handling mechanism by writing your own error checking macro or inline function. See the topic, [Visual C++ Extensions](./visual-c-extensions-for-ado.md), or the code in the following sections for examples.  
  
## Visual C++ Equivalents of Visual Basic Conventions  
 The following is a summary of several conventions in the ADO documentation, coded in Visual Basic, as well as their equivalents in Visual C++.  
  
### Declaring an ADO Object  
 In Visual Basic, an ADO object variable (in this case for a **Recordset** object) is declared as follows:  
  
```vb
Dim rst As ADODB.Recordset  
```
  
 The clause, "`ADODB.Recordset`", is the ProgID of the **Recordset** object as defined in the registry. A new instance of a **Record** object is declared as follows:  
  
```vb
Dim rst As New ADODB.Recordset  
```
  
 -or-  
  
```vb
Dim rst As ADODB.Recordset  
Set rst = New ADODB.Recordset  
```
  
 In Visual C++, the **#import** directive generates smart pointer-type declarations for all the ADO objects. For example, a variable that points to a **_Recordset** object is of type **_RecordsetPtr**, and is declared as follows:  
  
```cpp
_RecordsetPtr  rs;  
```
  
 A variable that points to a new instance of a **_Recordset** object is declared as follows:  
  
```cpp
_RecordsetPtr  rs("ADODB.Recordset");  
```
  
 -or-  
  
```cpp
_RecordsetPtr  rs;  
rs.CreateInstance("ADODB.Recordset");  
```
  
 -or-  
  
```cpp
_RecordsetPtr  rs;  
rs.CreateInstance(__uuidof(_Recordset));  
```
  
 After the **CreateInstance** method is called, the variable can be used as follows:  
  
```cpp
rs->Open(...);  
```
  
 Notice that in one case, the "`.`" operator is used as if the variable were an instance of a class (`rs.CreateInstance`), and in another case, the "`->`" operator is used as if the variable were a pointer to an interface (`rs->Open`).  
  
 One variable can be used in two ways because the "`->`" operator is overloaded to allow an instance of a class to behave like a pointer to an interface. A private class member of the instance variable contains a pointer to the **_Recordset** interface; the "`->`" operator returns that pointer; and the returned pointer accesses the members of the **_Recordset** object.  
  
### Coding a Missing Parameter - String  
 When you need to code a missing **String** operand in Visual Basic, you merely omit the operand. You must specify the operand in Visual C++. Code a **_bstr_t** that has an empty string as a value.  
  
```cpp
_bstr_t strMissing(L"");  
```
  
### Coding a Missing Parameter - Variant  
 When you need to code a missing **Variant** operand in Visual Basic, you merely omit the operand. You must specify all operands in Visual C++. Code a missing **Variant** parameter with a **_variant_t** set to the special value, DISP_E_PARAMNOTFOUND, and type, VT_ERROR. Alternatively, specify **vtMissing**, which is an equivalent predefined constant supplied by the **#import** directive.  
  
```cpp
_variant_t  vtMissingYours(DISP_E_PARAMNOTFOUND, VT_ERROR);   
```
  
 -or use -  
  
```cpp
...vtMissing...;  
```
  
### Declaring a Variant  
 In Visual Basic, a **Variant** is declared with the **Dim** statement as follows:  
  
```vb
Dim VariableName As Variant  
```
  
 In Visual C++, declare a variable as type **_variant_t**. A few schematic **_variant_t** declarations are shown below.  
  
> [!NOTE]
>  These declarations merely give a rough idea of what you would code in your own program. For more information, see the examples below, and the Visual C++documentation.  
  
```cpp
_variant_t  VariableName(value);  
_variant_t  VariableName((data type cast) value);  
_variant_t  VariableName(value, VT_DATATYPE);  
_variant_t  VariableName(interface * value, bool fAddRef = true);  
```
  
### Using Arrays of Variants  
 In Visual Basic, arrays of **Variants** can be coded with the **Dim** statement, or you may use the **Array** function, as demonstrated in the following example code:  
  
```vb
Public Sub ArrayOfVariants  
Dim cn As ADODB.Connection  
Dim rs As ADODB.Recordset  
Dim fld As ADODB.Field  
  
    cn.Open "DSN=pubs"  
    rs = cn.OpenSchema(adSchemaColumns, _  
        Array(Empty, Empty, "authors", Empty))  
    For Each fld in rs.Fields  
        Debug.Print "Name = "; fld.Name  
    Next fld  
    rs.Close  
    cn.Close  
End Sub  
```
  
 The following Visual C++ example demonstrates using a **SafeArray** used with a **_variant_t**.  
  
#### Notes  
 The following notes correspond to commented sections in the code example.  
  
1.  Once again, the TESTHR() inline function is defined to take advantage of the existing error-handling mechanism.  
  
2.  You only need a one-dimensional array, so you can use **SafeArrayCreateVector**, instead of the general purpose **SAFEARRAYBOUND** declaration and **SafeArrayCreate** function. The following is what that code would look like using **SafeArrayCreate**:  
  
    ```cpp
       SAFEARRAYBOUND   sabound[1];  
       sabound[0].lLbound = 0;  
       sabound[0].cElements = 4;  
       pSa = SafeArrayCreate(VT_VARIANT, 1, sabound);  
    ```
  
3.  The schema identified by the enumerated constant, **adSchemaColumns**, is associated with four constraint columns: TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, and COLUMN_NAME. Therefore, an array of **Variant** values with four elements is created. Then a constraint value that corresponds to the third column, TABLE_NAME, is specified.  
  
     The **Recordset** that is returned consists of several columns, a subset of which is the constraint columns. The values of the constraint columns for each returned row must be the same as the corresponding constraint values.  
  
4.  Those familiar with **SafeArrays** may be surprised that **SafeArrayDestroy**() is not called before the exit. In fact, calling **SafeArrayDestroy**() in this case will cause a run-time exception. The reason is that the destructor for `vtCriteria` will call **VariantClear**() when the **_variant_t** goes out of scope, which will free the **SafeArray**. Calling **SafeArrayDestroy**, without manually clearing the **_variant_t**, would cause the destructor to try to clear an invalid **SafeArray** pointer.  
  
     If **SafeArrayDestroy** were called, the code would look like this:  
  
    ```cpp
          TESTHR(SafeArrayDestroy(pSa));  
       vtCriteria.vt = VT_EMPTY;  
          vtCriteria.parray = NULL;  
    ```
  
     However, it is much simpler to let the **_variant_t** manage the **SafeArray**.  
  
```cpp
// Visual_CPP_ADO_Prog_1.cpp  
// compile with: /EHsc  
#import "msado15.dll" no_namespace rename("EOF", "EndOfFile")  
  
// Note 1  
inline void TESTHR( HRESULT _hr ) {   
   if FAILED(_hr)   
      _com_issue_error(_hr);   
}  
  
int main() {  
   CoInitialize(NULL);  
   try {  
      _RecordsetPtr pRs("ADODB.Recordset");  
      _ConnectionPtr pCn("ADODB.Connection");  
      _variant_t vtTableName("authors"), vtCriteria;  
      long ix[1];  
      SAFEARRAY *pSa = NULL;  
  
      pCn->Provider = "sqloledb";  
      pCn->Open("Data Source='(local)';Initial Catalog='pubs';Integrated Security=SSPI", "", "", adConnectUnspecified);  
      // Note 2, Note 3  
      pSa = SafeArrayCreateVector(VT_VARIANT, 1, 4);  
      if (!pSa)   
         _com_issue_error(E_OUTOFMEMORY);  
  
      // Specify TABLE_NAME in the third array element (index of 2).   
      ix[0] = 2;        
      TESTHR(SafeArrayPutElement(pSa, ix, &vtTableName));  
  
      // There is no Variant constructor for a SafeArray, so manually set the   
      // type (SafeArray of Variant) and value (pointer to a SafeArray).  
  
      vtCriteria.vt = VT_ARRAY | VT_VARIANT;  
      vtCriteria.parray = pSa;  
  
      pRs = pCn->OpenSchema(adSchemaColumns, vtCriteria, vtMissing);  
  
      long limit = pRs->GetFields()->Count;  
      for ( long x = 0 ; x < limit ; x++ )  
         printf( "%d: %s\n", x + 1, ((char*) pRs->GetFields()->Item[x]->Name) );  
      // Note 4  
      pRs->Close();  
      pCn->Close();  
   }  
   catch (_com_error &e) {  
      printf("Error:\n");  
      printf("Code = %08lx\n", e.Error());  
      printf("Code meaning = %s\n", (char*) e.ErrorMessage());  
      printf("Source = %s\n", (char*) e.Source());  
      printf("Description = %s\n", (char*) e.Description());  
   }  
   CoUninitialize();  
}  
```
  
### Using Property Get/Put/PutRef  
 In Visual Basic, the name of a property is not qualified by whether it is retrieved, assigned, or assigned a reference.  
  
```vb
Public Sub GetPutPutRef  
Dim rs As New ADODB.Recordset  
Dim cn As New ADODB.Connection  
Dim sz as Integer  
cn.Open "Provider=sqloledb;Data Source=yourserver;" & _  
         "Initial Catalog=pubs;Integrated Security=SSPI;"  
rs.PageSize = 10  
sz = rs.PageSize  
rs.ActiveConnection = cn  
rs.Open "authors",,adOpenStatic  
' ...  
rs.Close  
cn.Close  
End Sub  
```
  
 This Visual C++ example demonstrates the **Get**/**Put**/**PutRef**_Property_.  
  
#### Notes  
 The following notes correspond to commented sections in the code example.  
  
1.  This example uses two forms of a missing string argument: an explicit constant, **strMissing**, and a string that the compiler will use to create a temporary **_bstr_t** that will exist for the scope of the **Open** method.  
  
2.  It is not necessary to cast the operand of `rs->PutRefActiveConnection(cn)` to `(IDispatch *)` because the type of the operand is already `(IDispatch *)`.  
  
```cpp
// Visual_CPP_ado_prog_2.cpp  
// compile with: /EHsc  
#import "msado15.dll" no_namespace rename("EOF", "EndOfFile")  
  
int main() {  
   CoInitialize(NULL);  
   try {  
      _ConnectionPtr cn("ADODB.Connection");  
      _RecordsetPtr rs("ADODB.Recordset");  
      _bstr_t strMissing(L"");  
      long oldPgSz = 0, newPgSz = 5;  
  
      // Note 1  
      cn->Provider = "sqloledb";  
      cn->Open("Data Source='(local)';Initial Catalog=pubs;Integrated Security=SSPI;", strMissing, "", adConnectUnspecified);  
  
      oldPgSz = rs->GetPageSize();  
      // -or-  
      // oldPgSz = rs->PageSize;  
  
      rs->PutPageSize(newPgSz);  
      // -or-  
      // rs->PageSize = newPgSz;  
  
      // Note 2  
      rs->PutRefActiveConnection( cn );  
      rs->Open("authors", vtMissing, adOpenStatic, adLockReadOnly, adCmdTable);  
      printf("Original pagesize = %d, new pagesize = %d\n", oldPgSz, rs->GetPageSize());  
      rs->Close();  
      cn->Close();  
  
   }  
   catch (_com_error &e) {  
      printf("Description = %s\n", (char*) e.Description());  
   }  
   ::CoUninitialize();  
}  
```
  
### Using GetItem(x) and Item[x]  
 This Visual Basic example demonstrates the standard and alternative syntax for **Item**().  
  
```vb
Public Sub GetItemItem  
Dim rs As New ADODB.Recordset  
Dim name as String  
rs = rs.Open "authors", "DSN=pubs;", adOpenDynamic, _  
         adLockBatchOptimistic, adTable  
name = rs(0)  
' -or-  
name = rs.Fields.Item(0)  
rs(0) = "Test"  
rs.UpdateBatch  
' Restore name  
rs(0) = name  
rs.UpdateBatch  
rs.Close  
End Sub  
```
  
 This Visual C++ example demonstrates **Item**.  
  
> [!NOTE]
>  The following note corresponds to commented sections in the code example:  When the collection is accessed with **Item**, the index, **2**, must be cast to **long** so an appropriate constructor will be invoked.  
  
```cpp
// Visual_CPP_ado_prog_3.cpp  
// compile with: /EHsc  
#import "msado15.dll" no_namespace rename("EOF", "EndOfFile")  
  
void main() {  
   CoInitialize(NULL);  
   try {  
      _ConnectionPtr cn("ADODB.Connection");  
      _RecordsetPtr rs("ADODB.Recordset");  
      _variant_t vtFirstName;  
  
      cn->Provider = "sqloledb";  
      cn->Open("Data Source='(local)';Initial Catalog=pubs;Integrated Security=SSPI;", "", "", adConnectUnspecified);  
  
      rs->PutRefActiveConnection( cn );  
      rs->Open("authors", vtMissing, adOpenStatic, adLockOptimistic, adCmdTable);  
      rs->MoveFirst();  
  
      // Note 1. Get a field.  
      vtFirstName = rs->Fields->GetItem((long)2)->GetValue();  
      // -or-  
      vtFirstName = rs->Fields->Item[(long)2]->Value;  
  
      printf( "First name = '%s'\n", (char*)( (_bstr_t)vtFirstName) );  
  
      rs->Fields->GetItem((long)2)->Value = L"TEST";  
      rs->Update(vtMissing, vtMissing);  
  
      // Restore name  
      rs->Fields->GetItem((long)2)->PutValue(vtFirstName);  
      // -or-  
      rs->Fields->GetItem((long)2)->Value = vtFirstName;  
      rs->Update(vtMissing, vtMissing);  
      rs->Close();  
   }  
   catch (_com_error &e) {  
      printf("Description = '%s'\n", (char*) e.Description());  
   }  
   ::CoUninitialize();  
}  
```
  
### Casting ADO object pointers with (IDispatch *)  
 The following Visual C++ example demonstrates using (IDispatch *) to cast ADO object pointers.  
  
#### Notes  
 The following notes correspond to commented sections in the code example.  
  
1.  Specify an open **Connection** object in an explicitly coded **Variant**. Cast it with (IDispatch \*) so the correct constructor will be invoked. Also, explicitly set the second **_variant_t** parameter to the default value of **true**, so the object reference count will be correct when the **Recordset::Open** operation ends.  
  
2.  The expression, `(_bstr_t)`, is not a cast, but a **_variant_t** operator that extracts a **_bstr_t** string from the **Variant** returned by **Value**.  
  
 The expression, `(char*)`, is not a cast, but a **_bstr_t** operator that extracts a pointer to the encapsulated string in a **_bstr_t** object.  
  
 This section of code demonstrates some of the useful behaviors of **_variant_t** and **_bstr_t** operators.  
  
```cpp
// Visual_CPP_ado_prog_4.cpp  
// compile with: /EHsc  
#import "msado15.dll" no_namespace rename("EOF", "EndOfFile")  
  
int main() {  
   CoInitialize(NULL);  
   try {  
      _ConnectionPtr pConn("ADODB.Connection");  
      _RecordsetPtr pRst("ADODB.Recordset");  
  
      pConn->Provider = "sqloledb";  
      pConn->Open("Data Source='(local)';Initial Catalog='pubs';Integrated Security=SSPI", "", "", adConnectUnspecified);  
  
      // Note 1.  
      pRst->Open("authors", _variant_t((IDispatch *) pConn, true), adOpenStatic, adLockReadOnly, adCmdTable);  
      pRst->MoveLast();  
  
      // Note 2.  
      printf("Last name is '%s %s'\n",   
         (char*) ((_bstr_t) pRst->GetFields()->GetItem("au_fname")->GetValue()),  
         (char*) ((_bstr_t) pRst->Fields->Item["au_lname"]->Value));  
  
      pRst->Close();  
      pConn->Close();  
   }  
   catch (_com_error &e) {  
      printf("Description = '%s'\n", (char*) e.Description());  
   }     
   ::CoUninitialize();  
}  
```