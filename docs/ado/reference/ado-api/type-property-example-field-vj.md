---
title: "Type Property Example (Field) (VJ++) | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "drivers"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Type property [field] [ADO], VJ++ example"
ms.assetid: 65e19302-4682-41c8-ac7f-de1a4e23eb2b
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# Type Property Example (Field) (VJ++)
This example demonstrates the [Type](../../../ado/reference/ado-api/type-property-ado.md) property by displaying the name of the constant that corresponds to the value of the **Type** property of all the [Field](../../../ado/reference/ado-api/field-object.md) objects in the ***Employees*** table. The FieldType function is required for this procedure to run.  
  
```  
// BeginFieldTypeJ  
import java.io.*;  
import com.ms.wfc.data.*;  
  
public class TypeFieldX  
{  
  
   // The main entry point of the application.  
  
   public static void main (String[] args)  
   {  
      TypeFieldX();  
      System.exit(0);  
   }  
  
   // TypeFieldX Function  
   static void TypeFieldX()  
   {  
      // Define ADO Objects.  
      Recordset rstEmployees = null;  
      Field fldLoop = null;  
  
      // Declarations.  
      String strCnn = "Provider='sqloledb';Data Source='MySqlServer';"+  
                "Initial Catalog='Pubs';Integrated Security='SSPI';";  
      int intLoop;  
      int intRecordCount = 0;  
      BufferedReader in =   
         new BufferedReader(new InputStreamReader(System.in));  
  
      try  
      {  
         // Open the Recordset with data from Employees table.  
         rstEmployees = new Recordset();  
         rstEmployees.open("employee", strCnn,   
            AdoEnums.CursorType.FORWARDONLY, AdoEnums.LockType.READONLY,   
            AdoEnums.CommandType.TABLE);  
  
         System.out.println("Fields in the Employees table:\n");  
  
         // Enumerate fields collection of Employees table.  
         for(intLoop = 0;intLoop <   
            rstEmployees.getFields().getCount();intLoop++)  
         {  
            intRecordCount++;  
            // Loop needed for display of records  
            if((intRecordCount % 6)==0)  
            {  
               System.out.println("Press <Enter> to continue..");  
               in.readLine();  
            }  
  
            fldLoop = rstEmployees.getFields().getItem(intLoop);  
            System.out.println("  Name:" + fldLoop.getName() + "\n"+  
               "  Type:" + FieldType(fldLoop.getType()) + "\n");  
  
         }  
         System.out.println("Press <Enter> to continue");  
         in.readLine();  
      }  
      catch(AdoException ae)  
      {  
         // Notify the user of any errors that result from ADO.  
  
         // As passing a Recordset, check for the null pointer first.  
         if(rstEmployees != null)  
         {  
            PrintProviderError(rstEmployees.getActiveConnection());  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getMessage());  
         }  
      }  
      // System read requires this catch.  
      catch(java.io.IOException je)  
      {  
         PrintIOError(je);  
      }     
  
      finally  
      {  
         // Cleanup objects before exit.     
         if (rstEmployees != null)  
            if (rstEmployees.getState() == 1)  
               rstEmployees.close();  
      }  
   }  
   // FieldType Function  
   static String FieldType( int intType )  
   {  
      String strLoop = null;  
  
         switch(intType)  
         {  
         case AdoEnums.DataType.CHAR:  
            strLoop = "adChar";  
            break;  
         case AdoEnums.DataType.VARCHAR:  
            strLoop ="adVarChar";  
            break;  
         case AdoEnums.DataType.SMALLINT:  
            strLoop = "adSmallInt";  
            break;  
         case AdoEnums.DataType.UNSIGNEDTINYINT:  
            strLoop = "adUnsignedTinyInt" ;  
            break;  
         case AdoEnums.DataType.DBTIMESTAMP:  
            strLoop = "adDBTimeStamp";  
            break;  
         default:  
            break;  
         }  
  
      return strLoop;  
   }  
  
   // PrintProviderError Function  
   static void PrintProviderError(Connection cnn1)  
   {  
      // Print Provider Errors from Connection Object.  
      // ErrItem is an item object in the Connections Errors Collection.  
      com.ms.wfc.data.Error               ErrItem = null;  
      long                                 nCount = 0;  
      int                                       i = 0;  
  
      nCount = cnn1.getErrors().getCount();  
  
      // If there are any errors in the collection, print them.  
      if ( nCount > 0)  
      {  
         // Collection ranges from 0 to nCount-1.  
         for ( i=0;i<nCount; i++)  
         {  
            ErrItem = cnn1.getErrors().getItem(i);  
            System.out.println("\t Error Number: " + ErrItem.getNumber()   
               + "\t" + ErrItem.getDescription());  
         }  
      }  
   }  
   // PrintIOError Function  
   static void PrintIOError(java.io.IOException je)  
   {  
      System.out.println("Error: \n");  
      System.out.println("\t Source: " + je.getClass() + "\n");  
      System.out.println("\t Description: "+ je.getMessage() + "\n");  
   }  
}  
// EndFieldTypeJ  
  
```  
  
## See Also  
 [Field Object](../../../ado/reference/ado-api/field-object.md)   
 [Type Property (ADO)](../../../ado/reference/ado-api/type-property-ado.md)