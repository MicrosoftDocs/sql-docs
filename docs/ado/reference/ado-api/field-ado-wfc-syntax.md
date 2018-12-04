---
title: "Field (ADO - WFC Syntax) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "02/15/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
helpviewer_keywords: 
  - "Field collection [ADO], ADO/WFC syntax"
ms.assetid: 7e01cb24-2338-4f92-ad46-8d97248e1a4d
author: MightyPen
ms.author: genemi
manager: craigg
---
# Field (ADO - WFC Syntax)
## package com.ms.wfc.data  
  
### Methods  
  
```  
public void appendChunk(byte[] bytes)  
public void appendChunk(char[] chars)  
public void appendChunk(String chars)  
public byte[] getByteChunk(int len)  
public char[] getCharChunk(int len)  
public String getStringChunk(int len)  
```  
  
### Properties  
  
```  
public int getActualSize()  
public int getAttributes()  
public void setAttributes(int pl)  
public com.ms.com.IUnknown getDataFormat()  
public void setDataFormat(com.ms.com.IUnknown format)  
```  
  
 (For more information, see the documentation for the com.ms.wfc.data.IDataFormat interface.)  
  
```  
public int getDefinedSize()  
public void setDefinedSize(int pl)  
public String getName()  
public int getNumericScale()  
public void setNumericScale(byte pbNumericScale)  
public Variant getOriginalValue()  
public int getPrecision()  
public void setPrecision(byte pbPrecision)  
public int getType()  
public void setType(int pDataType)  
public Variant getUnderlyingValue()  
public Variant getValue()  
public void setValue(Variant value)  
public AdoProperties getProperties()  
```  
  
### Field Accessor Methods  
 The [Value](../../../ado/reference/ado-api/value-property-ado.md) property of a [Field](../../../ado/reference/ado-api/field-object.md) object gets or sets the content of that object. The content is represented as a VARIANT, a type of object that can be assigned a value and any of several data types.  
  
 ADO/WFC implements the **Value** property with the **getValue** method, which returns a VARIANT object; and the **setValue** method, which takes a VARIANT as an argument. VARIANTs are highly efficient in certain languages, such as Microsoft Visual Basic.  
  
 In addition to the **Value** property, ADO/WFC provides *accessor* methods that use Java data types to get and set the content of **Field** objects. Most of these methods have names of the form **get**_DataType_ or **set**_DataType_.  
  
 There are two noteworthy exceptions: One of the **getObject** methods returns an object coerced into a specified class. There is no **getNull** property; instead, there is an **isNull** property that returns a Boolean value indicating whether the field is null.  
  
```  
public native boolean getBoolean();  
public void setBoolean(boolean v)  
public native byte getByte();  
public void setByte(byte v)  
public native byte[] getBytes();  
public void setBytes(byte[] v)  
public native double getDouble();  
public void setDouble(double v)  
public native float getFloat();  
public void setFloat(float v)  
public native int getInt();  
public void setInt(int v)  
public native long getLong();  
public void setLong(long v)  
public native short getShort();  
public void setShort(short v)  
public native String getString();  
public void setString(String v)  
public native boolean isNull();  
public void setNull()  
public Object getObject()  
public Object getObject(Class c)  
public void setObject(Object value)  
```  
  
## See Also  
 [Field Object](../../../ado/reference/ado-api/field-object.md)
