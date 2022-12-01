---
title: "Preprocess a Schema to Merge Included Schemas"
description: Learn how XML schemas containing the xsd:include directive can be preprocessed to copy and merge the contents of any included schemas into a single schema.
ms.custom: ""
ms.date: 05/05/2022
ms.service: sql
ms.reviewer: randolphwest
ms.subservice: xml
ms.topic: conceptual
helpviewer_keywords:
  - "testing preprocessor tool"
  - "xsd:include"
  - "XML schema collections [SQL Server], preprocessor tool"
  - "include element"
  - "XML schemas [SQL Server], preprocessing"
  - "schema collections [SQL Server], preprocessor tool"
  - "preprocessor tool [XML schemas]"
  - "XML schemas [SQL Server]"
author: MikeRayMSFT
ms.author: mikeray
---
# Preprocess a schema to merge included schemas

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The W3C XSD **include** element provides support for schema modularity in which an XML schema can be partitioned into more than one physical file. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] currently doesn't support this element. XML schemas that include this element will be rejected by the server.

As a solution, XML schemas that include the `<xsd:include>` directive can be preprocessed to copy and merge the contents of any included schemas into a single schema for uploading to the server. The following C# code can be used for the preprocessing. The comments in the early part of the code provide information about how to use it.

```csharp
// XSD Schema Include Normalizer
// To compile:
// csc filename.cs
//
// How to use:
//
// Arguments: [-q] input.xsd [output.xsd]
//
// input.xsd       - file to normalize
// output.xsd      - file to output, default is console
// -q              - quiet
//
// Example:
//
// filename.exe schema.xsd
//
using System;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Collections;
public class XsdSchemaNormalizer
{
    private static bool NormalizeXmlSchema( String url, TextWriter writer )
    {
   try {
       XmlTextReader txtRead = new XmlTextReader( url );
       XmlSchema sch = XmlSchema.Read( txtRead, null );

       // Compiling Schema
       sch.Compile(null);

       XmlSchema outSch =
      XmlSchemaIncludeNormalizer.BuildIncludeFreeXmlSchema( sch);

       outSch.Write( writer );
   } catch ( Exception e ) {
       Console.WriteLine(e.ToString());
       return false;
   }
   return true;
    }
    public static void usage()
    {
   Console.WriteLine("Arguments: [-q] [-v] input.xsd [output.xsd]\n");
   Console.WriteLine("input.xsd       - file to normalize");
   Console.WriteLine("output.xsd      - file to output, default is console");
   Console.WriteLine("-q              - quiet");
    }
    public static void Main(String []args)
    {
   if( args.GetLength(0) < 1 ) {
       usage();
       return;
   }
   int argi = 0;
   bool quiet = false;
   if( args[argi] == "-q" ) {
       quiet = true;
            argi++;
   }

   if( argi == args.GetLength(0) )
   {
       usage();
       return;
   }

   String url = args[argi];

   if( !quiet )
       Console.WriteLine("Loading Schema: " + url);

   if( argi < ( args.GetLength(0) - 1 ) )
   {
       if( !quiet )
      Console.WriteLine("Outputing to file: " + args[argi+1]);

       StreamWriter output =
      new StreamWriter( new FileStream(args[argi+1], FileMode.Create ));

       NormalizeXmlSchema( url, output);
   }
   else
   {
       NormalizeXmlSchema( url, Console.Out);
   }

    }
}

// A class to remove all <include> from a Xml Schema
//
public class XmlSchemaIncludeNormalizer
{
    // Takes as input a XmlSchema which has includes in it
    // and the schema location uri of that XmlSchema
    //
    // Returns a "preprocessed" form of XmlSchema without any
    // includes. It still retains imports though. Also, it does
    // not propagate unhandled attributes
    //
    // It can throw any exception
    public static XmlSchema BuildIncludeFreeXmlSchema( XmlSchema inSch )
    {
   XmlSchema outSch = new XmlSchema();

   AddSchema( outSch, inSch );

   return outSch;
    }

    // Adds everything in the second schema minus includes to
    // the first schema
    //
    private static void AddSchema( XmlSchema outSch, XmlSchema add)
    {
   outSch.AttributeFormDefault = add.AttributeFormDefault;
   outSch.BlockDefault = add.BlockDefault;
   outSch.ElementFormDefault = add.ElementFormDefault;
   outSch.FinalDefault = add.FinalDefault;
   outSch.Id = add.Id;
   outSch.TargetNamespace = add.TargetNamespace;
   outSch.Version = add.Version;

   AddTableToSchema( outSch, add.AttributeGroups );
   AddTableToSchema( outSch, add.Attributes );
   AddTableToSchema( outSch, add.Elements );
   AddTableToSchema( outSch, add.Groups );
   AddTableToSchema( outSch, add.Notations );
   AddTableToSchema( outSch, add.SchemaTypes );

   // Handle includes as a special case
   for( int i = 0; i < add.Includes.Count; i++ )
   {
       if( ! ( add.Includes[i] is XmlSchemaInclude) )
      outSch.Includes.Add( add.Includes[i] );
   }
    }

    // Adds all items in the XmlSchemaObjectTable to the specified XmlSchema
    //
    private static void AddTableToSchema( XmlSchema outSch,
                 XmlSchemaObjectTable table )
    {
   IDictionaryEnumerator e = table.GetEnumerator();

   while( e.MoveNext() )
   {
       outSch.Items.Add( (XmlSchemaObject)e.Value );
   }
    }
}
```

## Test the preprocessor tool

You can use the following XSD schemas to test the preprocessor tool:

### books_common.xsd

```xml
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"
     xmlns="bookstore-schema"
     elementFormDefault="qualified" >
  <xsd:element name="publisher" type="xsd:string"/>
</xsd:schema>
```

### books.xsd

```xml
<xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema"
     xmlns="bookstore-schema"
     elementFormDefault="qualified"
     targetNamespace="bookstore-schema">
  <xsd:include id="books_common" schemaLocation="books_common.xsd"/>
  <xsd:element name="bookstore" type="xsd:string" />
</xsd:schema>
```

## See also

- [XML Schema Collections &#40;SQL Server&#41;](../../relational-databases/xml/xml-schema-collections-sql-server.md)
