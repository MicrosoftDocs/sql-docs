---
title: "Attributes and Name Properties Example (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Attributes property [ADO], VJ++ example"
  - "Name property [ADO], VJ++ example"
ms.assetid: 625f8bcb-a9bb-4534-8768-00a9bcbe7b7f
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# Attributes and Name Properties Example (VJ++)
This example displays the value of the [Attributes](../../../ado/reference/ado-api/attributes-property-ado.md) property for [Connection](../../../ado/reference/ado-api/connection-object-ado.md), [Field](../../../ado/reference/ado-api/field-object.md), and [Property](../../../ado/reference/ado-api/property-object-ado.md) objects. It uses the [Name](../../../ado/reference/ado-api/name-property-ado.md) property to display the name of each **Field** and **Property** object.  
  
```  
// BeginAttributesJ  
import com.ms.wfc.data.*;  
import java.io.*;  
  
public class AttributesX  
{  
   // The main entry point for the application  
   public static void main (String[] args)  
   {  
      AttributesX();  
      System.exit(0);  
   }  
  
   // AttributeX Function  
  
   static void AttributesX()  
   {  
      // Define ADO Objects.  
      Connection cnConn1 = null;  
      Recordset rstEmployees = null;  
      Fields listOfFields = null;  
      AdoProperties listOfProperties = null;  
  
      //Declarations.  
      BufferedReader in =   
         new BufferedReader (new InputStreamReader(System.in));  
      String line = null;  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"  
         + "Initial Catalog='Pubs';Integrated Security='SSPI';";  
      int recordDisplaySize = 15;  
      int propertyCount = 0;  
      try  
      {  
         // Open connection and recordset.  
         cnConn1 = new Connection();  
         cnConn1.open(strCnn);  
  
         rstEmployees = new Recordset ();  
         rstEmployees.open("employee",   
            cnConn1,AdoEnums.CursorType.FORWARDONLY,   
            AdoEnums.LockType.READONLY, AdoEnums.CommandType.TABLE);  
  
         // Display the attributes of the connection.  
         System.out.println("Connection attributes = "   
            + cnConn1.getAttributes());  
         // Display the attributes of the Employees table's fields.  
         System.out.println("\n\nField attributes : " + "\n");  
  
         listOfFields = rstEmployees.getFields();  
  
         for ( int i=0; i < listOfFields.getCount();i++)  
         {  
            System.out.println("\t\t" + listOfFields.getItem(i).getName()  
               + " = " + listOfFields.getItem(i).getAttributes());  
         }  
  
         // Display fields of the Employees table which are NULLABLE.  
         System.out.println("\n\nNULLABLE Fields : " + "\n");  
         for ( int i=0; i < listOfFields.getCount();i++)  
         {  
            if ((listOfFields.getItem(i).getAttributes() &   
               AdoEnums.FieldAttribute.ISNULLABLE) > 0)  
               System.out.println("\t\t" +   
               listOfFields.getItem(i).getName());  
  
         }  
  
         System.out.println ("\n\nPress <Enter> key to continue..");  
         line = in.readLine();  
  
         // Display the attributes of the Employees table's properties.  
         System.out.println("\n\nProperty attributes : " );  
         listOfProperties = rstEmployees.getProperties();  
         for ( int i=0; i < listOfProperties.getCount() ;i++)  
         {  
            System.out.println("\t\t" +   
               listOfProperties.getItem(i).getName()  
               + " = " + listOfProperties.getItem(i).getAttributes());  
  
            if (propertyCount == recordDisplaySize)  
            {  
               System.out.println ("\n\nPress <Enter> key to continue.");  
               line = in.readLine();  
               propertyCount = 0;  
            }  
            propertyCount++;  
         }  
  
         System.out.println ("\n\nPress <Enter> key to continue.");  
         line = in.readLine();  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
  
         // Check for null pointer for connection object.  
         if (rstEmployees.getActiveConnection()==null)  
            System.out.println("Exception: " + ae.getMessage());  
  
         // As passing a Recordset, check for null pointer first.  
  
         if (rstEmployees != null)  
         {  
            PrintProviderError(rstEmployees.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
      }  
  
      // System read requires this catch.  
      catch( java.io.IOException je)  
      {  
         PrintIOError(je);  
      }        
  
      finally  
      {  
         // Cleanup objects before exit.     
         if (rstEmployees != null)  
            if (rstEmployees.getState() == 1)  
               rstEmployees.close();     
         if (cnConn1 != null)  
            if (cnConn1.getState() == 1)  
               cnConn1.close();  
      }  
   }  
  
   // PrintProviderError Function  
  
   static void PrintProviderError( Connection Cnn1 )  
   {  
      // Print Provider errors from Connection object.  
      // ErrItem is an item object in the Connection's Errors collection.  
      com.ms.wfc.data.Error  ErrItem = null;  
      long nCount = 0;  
      int  i      = 0;  
  
      nCount = Cnn1.getErrors().getCount();  
  
      // If there are any errors in the collection, print them.  
      if( nCount > 0);  
      {  
         // Collection ranges from 0 to nCount - 1  
         for (i = 0; i< nCount; i++)  
         {  
            ErrItem = Cnn1.getErrors().getItem(i);  
            System.out.println("\t Error number: " + ErrItem.getNumber()  
               + "\t" + ErrItem.getDescription() );  
         }  
      }  
  
   }  
  
   //.PrintIOError Function  
  
   static void PrintIOError( java.io.IOException je)  
   {  
      System.out.println("Error \n");  
      System.out.println("\tSource = " + je.getClass() + "\n");  
      System.out.println("\tDescription = " + je.getMessage() + "\n");  
   }  
}  
  
// EndAttributesJ  
```  
  
## See Also  
 [Attributes Property (ADO)](../../../ado/reference/ado-api/attributes-property-ado.md)   
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)   
 [Field Object](../../../ado/reference/ado-api/field-object.md)   
 [Name Property (ADO)](../../../ado/reference/ado-api/name-property-ado.md)   
 [Property Object (ADO)](../../../ado/reference/ado-api/property-object-ado.md)