---
title: "Coding User-Defined Types"
description: This example shows how to implement a UDT to use in a SQL Server database. It implements the UDT as a structure.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "validation [CLR integration]"
  - "UDTs [CLR integration], coding"
  - "UserDefined serialization format [CLR integration]"
  - "Point UDT"
  - "Parse method"
  - "null values [CLR integration]"
  - "ToString method"
  - "ValidatePoint method"
  - "attributes [CLR integration]"
  - "coding user-defined types [CLR integration]"
  - "Read method"
  - "Write method"
  - "nullability [CLR integration]"
  - "user-defined types [CLR integration], coding"
  - "Currency UDT"
  - "validating UDT values"
  - "exposing UDT properties [CLR integration]"
dev_langs:
  - "VB"
  - "CSharp"
ms.assetid: 1e5b43b3-4971-45ee-a591-3f535e2ac722
---
# Creating User-Defined Types - Coding
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  When coding your user-defined type (UDT) definition, you must implement various features, depending on whether you are implementing the UDT as a class or a structure, as well as on the format and serialization options you have chosen.  
  
 The example in this section illustrates implementing a **Point** UDT as a **struct** (or **Structure** in Visual Basic). The **Point** UDT consists of X and Y coordinates implemented as property procedures.  
  
 The following namespaces are required when defining a UDT:  
  
```vb  
Imports System  
Imports System.Data.SqlTypes  
Imports Microsoft.SqlServer.Server  
```  
  
```csharp  
using System;  
using System.Data.SqlTypes;  
using Microsoft.SqlServer.Server;  
```  
  
 The **Microsoft.SqlServer.Server** namespace contains the objects required for various attributes of your UDT, and the **System.Data.SqlTypes** namespace contains the classes that represent [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] native data types available to the assembly. There may of course be additional namespaces that your assembly requires in order to function correctly. The **Point** UDT also uses the **System.Text** namespace for working with strings.  
  
> [!NOTE]  
>  Visual C++ database objects, such as UDTs, compiled with **/clr:pure** are not supported for execution.  
  
## Specifying Attributes  
 Attributes determine how serialization is used to construct the storage representation of UDTs and to transmit UDTs by value to the client.  
  
 The **Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute** is required. The **Serializable** attribute is optional. You can also specify the **Microsoft.SqlServer.Server.SqlFacetAttribute** to provide information about the return type of a UDT. For more information, see [Custom Attributes for CLR Routines](../../relational-databases/clr-integration/database-objects/clr-integration-custom-attributes-for-clr-routines.md).  
  
### Point UDT Attributes  
 The **Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute** sets the storage format for the **Point** UDT to **Native**. **IsByteOrdered** is set to **true**, which guarantees that the results of comparisons are the same in SQL Server as if the same comparison had taken place in managed code. The UDT implements the **System.Data.SqlTypes.INullable** interface to make the UDT null aware.  
  
 The following code fragment shows the attributes for the **Point** UDT.  
  
```vb  
<Serializable(), SqlUserDefinedTypeAttribute(Format.Native, _  
  IsByteOrdered:=True)> _  
  Public Structure Point  
    Implements INullable  
```  
  
```csharp  
[Serializable]  
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native,  
  IsByteOrdered=true)]  
public struct Point : INullable  
{  
```  
  
## Implementing Nullability  
 In addition to specifying the attributes for your assemblies correctly, your UDT must also support nullability. UDTs loaded into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are null-aware, but in order for the UDT to recognize a null value, the UDT must implement the **System.Data.SqlTypes.INullable** interface.  
  
 You must create a property named **IsNull**, which is needed to determine whether a value is null from within CLR code. When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] finds a null instance of a UDT, the UDT is persisted using normal null-handling methods. The server does not waste time serializing or deserializing the UDT if it does not have to, and it does not waste space to store a null UDT. This check for nulls is performed every time a UDT is brought over from the CLR, which means that using the [!INCLUDE[tsql](../../includes/tsql-md.md)] IS NULL construct to check for null UDTs should always work. The **IsNull** property is also used by the server to test whether an instance is null. Once the server determines that the UDT is null, it can use its native null handling.  
  
 The **get()** method of **IsNull** is not special-cased in any way. If a **Point** variable **\@p** is **Null**, then **\@p.IsNull** will, by default, evaluate to "NULL", not "1". This is because the **SqlMethod(OnNullCall)** attribute of the **IsNull get()** method defaults to false. Because the object is **Null**, when the property is requested the object is not deserialized, the method is not called, and a default value of "NULL" is returned.  
  
### Example  
 In the following example, the `is_Null` variable is private and holds the state of null for the instance of the UDT. Your code must maintain an appropriate value for `is_Null`. The UDT must also have a static property named **Null** that returns a null value instance of the UDT. This allows the UDT to return a null value if the instance is indeed null in the database.  
  
```vb  
Private is_Null As Boolean  
  
Public ReadOnly Property IsNull() As Boolean _  
   Implements INullable.IsNull  
    Get  
        Return (is_Null)  
    End Get  
End Property  
  
Public Shared ReadOnly Property Null() As Point  
    Get  
        Dim pt As New Point  
        pt.is_Null = True  
        Return (pt)  
    End Get  
End Property  
```  
  
```csharp  
private bool is_Null;  
  
public bool IsNull  
{  
    get  
    {  
        return (is_Null);  
    }  
}  
  
public static Point Null  
{  
    get  
    {  
        Point pt = new Point();  
        pt.is_Null = true;  
        return pt;  
    }  
}  
```  
  
### IS NULL vs. IsNull  
 Consider a table that contains the schema Points(id int, location Point), where **Point** is a CLR UDT, and the following queries:  
  
```  
--Query 1  
SELECT ID  
FROM Points  
WHERE NOT (location IS NULL) -- Or, WHERE location IS NOT NULL;  
```  
  
```  
--Query 2:  
SELECT ID  
FROM Points  
WHERE location.IsNull = 0;  
```  
  
 Both queries return the IDs of points with non-**Null** locations. In Query 1, normal null-handling is used, and there no deserialization of UDTs is required. Query 2, on the other hand, has to deserialize each non-**Null** object and call into the CLR to obtain the value of the **IsNull** property. Clearly, using **IS NULL** will exhibit better performance and there should never be a reason to read the **IsNull** property of a UDT from [!INCLUDE[tsql](../../includes/tsql-md.md)] code.  
  
 So, what is the use of the **IsNull** property? First, it is needed to determine whether a value is **Null** from within CLR code. Second, the server needs a way to test whether an instance is **Null**, so this property is used by the server. After it determines it is **Null**, then it can use its native null handling to handle it.  
  
## Implementing the Parse Method  
 The **Parse** and **ToString** methods allow for conversions to and from string representations of the UDT. The **Parse** method allows a string to be converted into a UDT. It must be declared as **static** (or **Shared** in Visual Basic), and take a parameter of type **System.Data.SqlTypes.SqlString**.  
  
 The following code implements the **Parse** method for the **Point** UDT, which separates out the X and Y coordinates. The **Parse** method has a single argument of type **System.Data.SqlTypes.SqlString**, and assumes that X and Y values are supplied as a comma-delimited string. Setting the **Microsoft.SqlServer.Server.SqlMethodAttribute.OnNullCall** attribute to **false** prevents the **Parse** method from being called from a null instance of Point.  
  
```vb  
<SqlMethod(OnNullCall:=False)> _  
Public Shared Function Parse(ByVal s As SqlString) As Point  
    If s.IsNull Then  
        Return Null  
    End If  
  
    ' Parse input string here to separate out points.  
    Dim pt As New Point()  
    Dim xy() As String = s.Value.Split(",".ToCharArray())  
    pt.X = Int32.Parse(xy(0))  
    pt.Y = Int32.Parse(xy(1))  
    Return pt  
End Function  
```  
  
```csharp  
[SqlMethod(OnNullCall = false)]  
public static Point Parse(SqlString s)  
{  
    if (s.IsNull)  
        return Null;  
  
    // Parse input string to separate out points.  
    Point pt = new Point();  
    string[] xy = s.Value.Split(",".ToCharArray());  
    pt.X = Int32.Parse(xy[0]);  
    pt.Y = Int32.Parse(xy[1]);  
    return pt;  
}  
```  
  
## Implementing the ToString Method  
 The **ToString** method converts the **Point** UDT to a string value. In this case, the string "NULL" is returned for a Null instance of the **Point** type. The **ToString** method reverses the **Parse** method by using a **System.Text.StringBuilder** to return a comma-delimited **System.String** consisting of the X and Y coordinate values. Because **InvokeIfReceiverIsNull** defaults to false, the check for a null instance of **Point** is unnecessary.  
  
```vb  
Private _x As Int32  
Private _y As Int32  
  
Public Overrides Function ToString() As String  
    If Me.IsNull Then  
        Return "NULL"  
    Else  
        Dim builder As StringBuilder = New StringBuilder  
        builder.Append(_x)  
        builder.Append(",")  
        builder.Append(_y)  
        Return builder.ToString  
    End If  
End Function  
```  
  
```csharp  
private Int32 _x;  
private Int32 _y;  
  
public override string ToString()  
{  
    if (this.IsNull)  
        return "NULL";  
    else  
    {  
        StringBuilder builder = new StringBuilder();  
        builder.Append(_x);  
        builder.Append(",");  
        builder.Append(_y);  
        return builder.ToString();  
    }  
}  
```  
  
## Exposing UDT Properties  
 The **Point** UDT exposes X and Y coordinates that are implemented as public read-write properties of type **System.Int32**.  
  
```vb  
Public Property X() As Int32  
    Get  
        Return (Me._x)  
    End Get  
  
    Set(ByVal Value As Int32)  
        _x = Value  
    End Set  
End Property  
  
Public Property Y() As Int32  
    Get  
        Return (Me._y)  
    End Get  
  
    Set(ByVal Value As Int32)  
        _y = Value  
    End Set  
End Property  
```  
  
```csharp  
public Int32 X  
{  
    get  
    {  
        return this._x;  
    }  
    set   
    {  
        _x = value;  
    }  
}  
  
public Int32 Y  
{  
    get  
    {  
        return this._y;  
    }  
    set  
    {  
        _y = value;  
    }  
}  
```  
  
## Validating UDT Values  
 When working with UDT data, [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] automatically converts binary values to UDT values. This conversion process involves checking that values are appropriate for the serialization format of the type and ensuring that the value can be deserialized correctly. This ensures that the value can be converted back to binary form. In the case of byte-ordered UDTs, this also ensures that the resulting binary value matches the original binary value. This prevents invalid values from being persisted in the database. In some cases, this level of checking may be inadequate. Additional validation may be required when UDT values are required to be in an expected domain or range. For example, a UDT that implements a date might require the day value to be a positive number that falls within a certain range of valid values.  
  
 The **Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute.ValidationMethodName** property of the **Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute** allows you to supply the name of a validation method that the server runs when data is assigned to a UDT or converted to a UDT. **ValidationMethodName** is also called during the running of the bcp utility, BULK INSERT, DBCC CHECKDB, DBCC CHECKFILEGROUP, DBCC CHECKTABLE, distributed query, and tabular data stream (TDS) remote procedure call (RPC) operations. The default value for **ValidationMethodName** is null, indicating that there is no validation method.  
  
### Example  
 The following code fragment shows the declaration for the **Point** class, which specifies a **ValidationMethodName** of **ValidatePoint**.  
  
```vb  
<Serializable(), SqlUserDefinedTypeAttribute(Format.Native, _  
  IsByteOrdered:=True, _  
  ValidationMethodName:="ValidatePoint")> _  
  Public Structure Point  
```  
  
```csharp  
[Serializable]  
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native,  
  IsByteOrdered=true,   
  ValidationMethodName = "ValidatePoint")]  
public struct Point : INullable  
{  
```  
  
 If a validation method is specified, it must have a signature that looks like the following code fragment.  
  
```vb  
Private Function ValidationFunction() As Boolean  
    If (validation logic here) Then  
        Return True  
    Else  
        Return False  
    End If  
End Function  
```  
  
```csharp  
private bool ValidationFunction()  
{  
    if (validation logic here)  
    {  
        return true;  
    }  
    else  
    {  
        return false;  
    }  
}  
```  
  
 The validation method can have any scope and should return **true** if the value is valid, and **false** otherwise. If the method returns **false** or throws an exception, the value is treated as not valid and an error is raised.  
  
 In the example below, the code allows only values of zero or greater the X and Y coordinates.  
  
```vb  
Private Function ValidatePoint() As Boolean  
    If (_x >= 0) And (_y >= 0) Then  
        Return True  
    Else  
        Return False  
    End If  
End Function  
```  
  
```csharp  
private bool ValidatePoint()  
{  
    if ((_x >= 0) && (_y >= 0))  
    {  
        return true;  
    }  
    else  
    {  
        return false;  
    }  
}  
```  
  
### Validation Method Limitations  
 The server calls the validation method when the server is performing conversions, not when data is inserted by setting individual properties or when data is inserted using a [!INCLUDE[tsql](../../includes/tsql-md.md)] INSERT statement.  
  
 You must explicitly call the validation method from property setters and the **Parse** method if you want the validation method to execute in all situations. This is not a requirement, and in some cases may not even be desirable.  
  
### Parse Validation Example  
 To ensure that the **ValidatePoint** method is invoked in the **Point** class, you must call it from the **Parse** method and from the property procedures that set the X and Y coordinate values. The following code fragment shows how to call the **ValidatePoint** validation method from the **Parse** function.  
  
```vb  
<SqlMethod(OnNullCall:=False)> _  
Public Shared Function Parse(ByVal s As SqlString) As Point  
    If s.IsNull Then  
        Return Null  
    End If  
  
    ' Parse input string here to separate out points.  
    Dim pt As New Point()  
    Dim xy() As String = s.Value.Split(",".ToCharArray())  
    pt.X = Int32.Parse(xy(0))  
    pt.Y = Int32.Parse(xy(1))  
  
    ' Call ValidatePoint to enforce validation  
    ' for string conversions.  
    If Not pt.ValidatePoint() Then  
        Throw New ArgumentException("Invalid XY coordinate values.")  
    End If  
    Return pt  
End Function  
```  
  
```csharp  
[SqlMethod(OnNullCall = false)]  
public static Point Parse(SqlString s)  
{  
    if (s.IsNull)  
        return Null;  
  
    // Parse input string to separate out points.  
    Point pt = new Point();  
    string[] xy = s.Value.Split(",".ToCharArray());  
    pt.X = Int32.Parse(xy[0]);  
    pt.Y = Int32.Parse(xy[1]);  
  
    // Call ValidatePoint to enforce validation  
    // for string conversions.  
    if (!pt.ValidatePoint())   
        throw new ArgumentException("Invalid XY coordinate values.");  
    return pt;  
}  
```  
  
### Property Validation Example  
 The following code fragment shows how to call the **ValidatePoint** validation method from the property procedures that set the X and Y coordinates.  
  
```vb  
Public Property X() As Int32  
    Get  
        Return (Me._x)  
    End Get  
  
    Set(ByVal Value As Int32)  
        Dim temp As Int32 = _x  
        _x = Value  
        If Not ValidatePoint() Then  
            _x = temp  
            Throw New ArgumentException("Invalid X coordinate value.")  
        End If  
    End Set  
End Property  
  
Public Property Y() As Int32  
    Get  
        Return (Me._y)  
    End Get  
  
    Set(ByVal Value As Int32)  
        Dim temp As Int32 = _y  
        _y = Value  
        If Not ValidatePoint() Then  
            _y = temp  
            Throw New ArgumentException("Invalid Y coordinate value.")  
        End If  
    End Set  
End Property  
```  
  
```csharp  
    public Int32 X  
{  
    get  
    {  
        return this._x;  
    }  
    // Call ValidatePoint to ensure valid range of Point values.  
    set   
    {  
        Int32 temp = _x;  
        _x = value;  
        if (!ValidatePoint())  
        {  
            _x = temp;  
            throw new ArgumentException("Invalid X coordinate value.");  
        }  
    }  
}  
  
public Int32 Y  
{  
    get  
    {  
        return this._y;  
    }  
    set  
    {  
        Int32 temp = _y;  
        _y = value;  
        if (!ValidatePoint())  
        {  
            _y = temp;  
            throw new ArgumentException("Invalid Y coordinate value.");  
        }  
    }  
}  
```  
  
## Coding UDT Methods  
 When coding UDT methods, consider whether the algorithm used could possibly change over time. If so, you may want to consider creating a separate class for the methods your UDT uses. If the algorithm changes, you can recompile the class with the new code and load the assembly into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] without affecting the UDT. In many cases UDTs can be reloaded using the [!INCLUDE[tsql](../../includes/tsql-md.md)] ALTER ASSEMBLY statement, but that could potentially cause problems with existing data. For example, the **Currency** UDT included with the **AdventureWorks** sample database uses a **ConvertCurrency** function to convert currency values, which is implemented in a separate class. It is possible that conversion algorithms may change in unpredictable ways in the future, or that new functionality may be required. Separating the **ConvertCurrency** function from the **Currency** UDT implementation provides greater flexibility when planning for future changes.  
  
### Example  
 The **Point** class contains three simple methods for calculating distance: **Distance**, **DistanceFrom** and **DistanceFromXY**. Each returns a **double** calculating the distance from **Point** to zero, the distance from a specified point to **Point**, and the distance from specified X and Y coordinates to **Point**. **Distance** and **DistanceFrom** each call **DistanceFromXY**, and demonstrate how to use different arguments for each method.  
  
```vb  
' Distance from 0 to Point.  
<SqlMethod(OnNullCall:=False)> _  
Public Function Distance() As Double  
    Return DistanceFromXY(0, 0)  
End Function  
  
' Distance from Point to the specified point.  
<SqlMethod(OnNullCall:=False)> _  
Public Function DistanceFrom(ByVal pFrom As Point) As Double  
    Return DistanceFromXY(pFrom.X, pFrom.Y)  
End Function  
  
' Distance from Point to the specified x and y values.  
<SqlMethod(OnNullCall:=False)> _  
Public Function DistanceFromXY(ByVal ix As Int32, ByVal iy As Int32) _  
    As Double  
    Return Math.Sqrt(Math.Pow(ix - _x, 2.0) + Math.Pow(iy - _y, 2.0))  
End Function  
```  
  
```csharp  
// Distance from 0 to Point.  
[SqlMethod(OnNullCall = false)]  
public Double Distance()  
{  
    return DistanceFromXY(0, 0);  
}  
  
// Distance from Point to the specified point.  
[SqlMethod(OnNullCall = false)]  
public Double DistanceFrom(Point pFrom)  
{  
    return DistanceFromXY(pFrom.X, pFrom.Y);  
}  
  
// Distance from Point to the specified x and y values.  
[SqlMethod(OnNullCall = false)]  
public Double DistanceFromXY(Int32 iX, Int32 iY)  
{  
    return Math.Sqrt(Math.Pow(iX - _x, 2.0) + Math.Pow(iY - _y, 2.0));  
}  
```  
  
### Using SqlMethod Attributes  
 The **Microsoft.SqlServer.Server.SqlMethodAttribute** class provides custom attributes that can be used to mark method definitions in order to specify determinism, on null call behavior, and to specify whether a method is a mutator. Default values for these properties are assumed, and the custom attribute is only used when a non-default value is needed.  
  
> [!NOTE]  
>  The **SqlMethodAttribute** class inherits from the **SqlFunctionAttribute** class, so **SqlMethodAttribute** inherits the **FillRowMethodName** and **TableDefinition** fields from **SqlFunctionAttribute**. This implies that it is possible to write a table-valued method, which is not the case. The method compiles and the assembly deploys, but an error about the **IEnumerable** return type is raised at runtime with the following message: "Method, property, or field '\<name>' in class '\<class>' in assembly '\<assembly>' has invalid return type."  
  
 The following table describes some of the relevant **Microsoft.SqlServer.Server.SqlMethodAttribute** properties that can be used in UDT methods, and lists their default values.  
  
 DataAccess  
 Indicates whether the function involves access to user data stored in the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Default is **DataAccessKind**.**None**.  
  
 IsDeterministic  
 Indicates whether the function produces the same output values given the same input values and the same database state. Default is **false**.  
  
 IsMutator  
 Indicates whether the method causes a state change in the UDT instance. Default is **false**.  
  
 IsPrecise  
 Indicates whether the function involves imprecise computations, such as floating point operations. Default is **false**.  
  
 OnNullCall  
 Indicates whether the method is called when null reference input arguments are specified. Default is **true**.  
  
### Example  
 The **Microsoft.SqlServer.Server.SqlMethodAttribute.IsMutator** property allows you to mark a method that allows a change in the state of an instance of a UDT. [!INCLUDE[tsql](../../includes/tsql-md.md)] does not allow you to set two UDT properties in the SET clause of one UPDATE statement. However, you can have a method marked as a mutator that changes the two members.  
  
> [!NOTE]  
>  Mutator methods are not allowed in queries. They can be called only in assignment statements or data modification statements. If a method marked as mutator does not return **void** (or is not a **Sub** in Visual Basic), CREATE TYPE fails with an error.  
  
 The following statement assumes the existence of a **Triangles** UDT that has a **Rotate** method. The following [!INCLUDE[tsql](../../includes/tsql-md.md)] update statement invokes the **Rotate** method:  
  
```  
UPDATE Triangles SET t.RotateY(0.6) WHERE id=5  
```  
  
 The **Rotate** method is decorated with the **SqlMethod** attribute setting **IsMutator** to **true** so that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can mark the method as a mutator method. The code also sets **OnNullCall** to **false**, which indicates to the server that the method returns a null reference (**Nothing** in Visual Basic) if any of the input parameters are null references.  
  
```vb  
<SqlMethod(IsMutator:=True, OnNullCall:=False)> _  
Public Sub Rotate(ByVal anglex as Double, _  
  ByVal angley as Double, ByVal anglez As Double)   
   RotateX(anglex)  
   RotateY(angley)  
   RotateZ(anglez)  
End Sub  
```  
  
```csharp  
[SqlMethod(IsMutator = true, OnNullCall = false)]  
public void Rotate(double anglex, double angley, double anglez)   
{  
   RotateX(anglex);  
   RotateY(angley);  
   RotateZ(anglez);  
}  
```  
  
## Implementing a UDT with a User-Defined Format  
 When implementing a UDT with a user-defined format, you must implement **Read** and **Write** methods that implement the Microsoft.SqlServer.Server.IBinarySerialize interface to handle serializing and deserializing UDT data. You must also specify the **MaxByteSize** property of the **Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute**.  
  
### The Currency UDT  
 The **Currency** UDT is included with the CLR samples that can be installed with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], beginning with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
  
 The **Currency** UDT supports handling amounts of money in the monetary system of a particular culture. You must define two fields: a **string** for **CultureInfo**, which specifies who issued the currency (en-us, for example) and a **decimal** for **CurrencyValue**, the amount of money.  
  
 Although it is not used by the server for performing comparisons, the **Currency** UDT implements the **System.IComparable** interface, which exposes a single method, **System.IComparable.CompareTo**. This is used on the client side in situations where it is desirable to accurately compare or order currency values within cultures.  
  
 Code running in the CLR compares the culture separately from the currency value. For [!INCLUDE[tsql](../../includes/tsql-md.md)] code, the following actions determine the comparison:  
  
1.  Set the **IsByteOrdered** attribute to true, which tells [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use the persisted binary representation on disk for comparisons.  
  
2.  Use the **Write** method for the **Currency** UDT to determine how the UDT is persisted on disk and therefore how UDT values are compared and ordered for [!INCLUDE[tsql](../../includes/tsql-md.md)] operations.  
  
3.  Save the **Currency** UDT using the following binary format:  

    1.  Save the culture as a UTF-16 encoded string for bytes 0-19 with padding to the right with null characters.  
  
    2.  Use bytes 20 and above to contain the decimal value of the currency.  
  
 The purpose of the padding is to ensure that the culture is completely separated from the currency value, so that when one UDT is compared against another in [!INCLUDE[tsql](../../includes/tsql-md.md)] code, culture bytes are compared against culture bytes, and currency byte values are compared against currency byte values.  
  
### Currency Attributes  
 The **Currency** UDT is defined with the following attributes.  
  
```vb  
<Serializable(), Microsoft.SqlServer.Server.SqlUserDefinedType( _  
    Microsoft.SqlServer.Server.Format.UserDefined, _  
    IsByteOrdered:=True, MaxByteSize:=32), _  
    CLSCompliant(False)> _  
Public Structure Currency  
Implements INullable, IComparable, _  
Microsoft.SqlServer.Server.IBinarySerialize  
```  
  
```csharp  
[Serializable]  
[SqlUserDefinedType(Format.UserDefined,   
    IsByteOrdered = true, MaxByteSize = 32)]  
    [CLSCompliant(false)]  
    public struct Currency : INullable, IComparable, IBinarySerialize  
    {  
```  
  
## Creating Read and Write Methods with IBinarySerialize  
 When you choose **UserDefined** serialization format, you also must implement the **IBinarySerialize** interface and create your own **Read** and **Write** methods. The following procedures from the **Currency** UDT use the **System.IO.BinaryReader** and **System.IO.BinaryWriter** to read from and write to the UDT.  
  
```vb  
' IBinarySerialize methods  
' The binary layout is as follow:  
'    Bytes 0 - 19: Culture name, padded to the right with null  
'    characters, UTF-16 encoded  
'    Bytes 20+: Decimal value of money  
' If the culture name is empty, the currency is null.  
Public Sub Write(ByVal w As System.IO.BinaryWriter) _  
  Implements Microsoft.SqlServer.Server.IBinarySerialize.Write  
    If Me.IsNull Then  
        w.Write(nullMarker)  
        w.Write(System.Convert.ToDecimal(0))  
        Return  
    End If  
  
    If cultureName.Length > cultureNameMaxSize Then  
        Throw New ApplicationException(String.Format(CultureInfo.CurrentUICulture, _  
           "{0} is an invalid culture name for currency as it is too long.", cultureNameMaxSize))  
    End If  
  
    Dim paddedName As String = cultureName.PadRight(cultureNameMaxSize, CChar(vbNullChar))  
  
    For i As Integer = 0 To cultureNameMaxSize - 1  
        w.Write(paddedName(i))  
    Next i  
  
    ' Normalize decimal value to two places  
    currencyVal = Decimal.Floor(currencyVal * 100) / 100  
    w.Write(currencyVal)  
End Sub  
  
Public Sub Read(ByVal r As System.IO.BinaryReader) _  
  Implements Microsoft.SqlServer.Server.IBinarySerialize.Read  
    Dim name As Char() = r.ReadChars(cultureNameMaxSize)  
    Dim stringEnd As Integer = Array.IndexOf(name, CChar(vbNullChar))  
  
    If stringEnd = 0 Then  
        cultureName = Nothing  
        Return  
    End If  
  
    cultureName = New String(name, 0, stringEnd)  
    currencyVal = r.ReadDecimal()  
End Sub  
  
```  
  
```csharp  
// IBinarySerialize methods  
// The binary layout is as follow:  
//    Bytes 0 - 19:Culture name, padded to the right   
//    with null characters, UTF-16 encoded  
//    Bytes 20+:Decimal value of money  
// If the culture name is empty, the currency is null.  
public void Write(System.IO.BinaryWriter w)  
{  
    if (this.IsNull)  
    {  
        w.Write(nullMarker);  
        w.Write((decimal)0);  
        return;  
    }  
  
    if (cultureName.Length > cultureNameMaxSize)  
    {  
        throw new ApplicationException(string.Format(  
            CultureInfo.InvariantCulture,   
            "{0} is an invalid culture name for currency as it is too long.",   
            cultureNameMaxSize));  
    }  
  
    String paddedName = cultureName.PadRight(cultureNameMaxSize, '\0');  
    for (int i = 0; i < cultureNameMaxSize; i++)  
    {  
        w.Write(paddedName[i]);  
    }  
  
    // Normalize decimal value to two places  
    currencyValue = Decimal.Floor(currencyValue * 100) / 100;  
    w.Write(currencyValue);  
}  
public void Read(System.IO.BinaryReader r)  
{  
    char[] name = r.ReadChars(cultureNameMaxSize);  
    int stringEnd = Array.IndexOf(name, '\0');  
  
    if (stringEnd == 0)  
    {  
        cultureName = null;  
        return;  
    }  
  
    cultureName = new String(name, 0, stringEnd);  
    currencyValue = r.ReadDecimal();  
}  
```  
  
## See Also  
 [Creating a User-Defined Type](../../relational-databases/clr-integration-database-objects-user-defined-types/creating-user-defined-types.md)  
