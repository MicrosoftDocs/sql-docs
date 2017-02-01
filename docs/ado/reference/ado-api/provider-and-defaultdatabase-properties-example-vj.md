---
title: "Provider and DefaultDatabase Properties Example (VJ++) | Microsoft Docs"
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
  - "DefaultDatabase property [ADO], VJ++ example"
  - "provider property [ADO], VJ++ example"
ms.assetid: fdc26576-37d0-4fa1-9afa-75d0e7133675
caps.latest.revision: 10
author: "MightyPen"
ms.author: "annemill"
manager: "jhubbard"
---
# Provider and DefaultDatabase Properties Example (VJ++)
This example demonstrates the [Provider](../../../ado/reference/ado-api/provider-property-ado.md) property by opening three [Connection](../../../ado/reference/ado-api/connection-object-ado.md) objects using different providers. It also uses the [DefaultDatabase](../../../ado/reference/ado-api/defaultdatabase-property.md) property to set the default database for the Microsoft ODBC Provider.  
  
```  
// BeginProviderJ  
import java.io.*;  
import com.ms.wfc.data.*;  
  
public class ProviderX  
{  
   //    The main entry point of the application.  
   public static void main (String[] args)  
   {  
      ProviderX();  
      System.exit(0);  
   }  
   // ProviderX Function  
   static void ProviderX()  
   {  
      // Define ADO Objects.  
      Connection cnn1 = null;  
      Connection cnn2 = null;  
      Connection cnn3 = null;  
  
      // Declarations.  
      BufferedReader in =   
         new BufferedReader(new InputStreamReader(System.in));  
      try  
      {  
         // Open a connection using the Microsoft ODBC Provider.  
         cnn1 = new Connection();  
         cnn1.setConnectionString("driver={SQL Server};"+  
             "server='MySqlServer';User ID='MyUserID';Password='MyPassword';");  
         cnn1.open();  
         cnn1.setDefaultDatabase("Pubs");  
  
         // Display the provider.  
         System.out.println("\n\n\tCnn1 provider: "+ cnn1.getProvider());  
  
         // Open connection using the OLE DB Provider for Microsoft Jet.  
         cnn2 = new Connection();  
         cnn2.setProvider("Microsoft.Jet.OLEDB.4.0");  
         cnn2.open("Northwind.mdb","admin","");  
  
         // Display the provider.  
         System.out.println("\n\n\tCnn2 provider: " +   
            cnn2.getProvider());  
  
         // Open a connection using the Microsoft SQL Server Provider.  
         cnn3 = new Connection();  
         cnn3.setProvider("sqloledb");  
         cnn3.open("Data Source='MySqlServer';Initial Catalog='Pubs';Integrated Security='SSPI';","","");  
  
         // Display the provider.  
         System.out.println("\n\n\tCnn3 provider: " +   
            cnn3.getProvider());  
  
         System.out.println("\n\n\tPress <Enter> to continue..");  
         in.readLine();  
      }  
      catch(AdoException ae)  
      {  
         // Notify the user of any errors that result from ADO.  
  
         // As passing a Connection, check for null pointer first.  
         if(cnn1 != null)  
         {  
            PrintProviderError(cnn1);  
         }  
         else  
         {  
            System.out.println("Exception: " + ae.getLocalizedMessage());  
         }  
      }  
      // System read requires needs this catch.  
      catch(java.io.IOException je)  
      {  
         PrintIOError(je);  
      }     
  
      finally  
      {  
         // Cleanup objects before exit.     
         if (cnn1 != null)  
            if (cnn1.getState() == 1)  
               cnn1.close();     
         if (cnn2 != null)  
            if (cnn2.getState() == 1)  
               cnn2.close();     
         if (cnn3 != null)  
            if (cnn3.getState() == 1)  
               cnn3.close();  
      }  
   }  
   // PrintProviderError Function  
  
   static void PrintProviderError( Connection Cnn1 )  
   {  
      // Print Provider errors from Connection object.  
      // ErrItem is an item object in the Connections Errors collection.  
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
  
   // PrintIOError Function  
  
   static void PrintIOError( java.io.IOException je)  
   {  
      System.out.println("Error \n");  
      System.out.println("\tSource = " + je.getClass() + "\n");  
      System.out.println("\tDescription = " + je.getMessage() + "\n");  
   }  
}  
// EndProviderJ  
  
```  
  
## See Also  
 [Connection Object (ADO)](../../../ado/reference/ado-api/connection-object-ado.md)   
 [DefaultDatabase Property](../../../ado/reference/ado-api/defaultdatabase-property.md)   
 [Provider Property (ADO)](../../../ado/reference/ado-api/provider-property-ado.md)