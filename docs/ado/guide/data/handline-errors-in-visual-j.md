---
title: "Handline Errors in Visual J++ | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Visual J++ error handling [ADO]"
  - "errors [ADO], Visual J++"
ms.assetid: 70be5873-b95c-47a6-a793-d97c5b41e7e4
caps.latest.revision: 5
author: "MightyPen"
ms.author: "annemill"
manager: "sonalm"
---
# Handline Errors in Visual J++
Handle ADO errors in your Microsoft® Visual J++® applications using a **try catch** block. Once an error has been thrown, you can iterate through the collection, successively handling each error. The following Visual J++ example shows a console application that deliberately causes an error.  
  
 When the **catch** block is activated, it calls the PrintProviderError function to display the errors. The PrintProviderError function iterates through the **Errors** collection and sends a line to the standard output device that describes each error in the collection.  
  
```  
// BeginErrorExampleVJ  
// The WFC class includes the ADO objects.  
  
import com.ms.wfc.data.*;  
import java.io.* ;  
  
public class ErrorExample  
{  
   /**  
    * The main entry point for the application.   
    *  
    * @param args Array of parameters passed to the application  
    * via the command line.  
    */  
   public static void main (String[] args)  
   {  
      ForceError();  
      System.exit(0);  
   }  
  
   static void DescriptionX()  
   {  
      BufferedReader in = new   
         BufferedReader(new InputStreamReader(System.in));  
  
     // Define ADO Objects.  
      Connection cnConn1 = null;  
  
      try  
      {  
         // Create an error by trying to  
       // Open a database that doesn't exist.  
         cnConn1 = new Connection();  
         cnConn1.open("nothing");  
      }  
      catch( AdoException ae )  
      {  
         // Notify user of any errors that result from ADO.  
         PrintProviderError(cnConn1);  
      }  
  
     try  
     {  
       System.out.println("\nPress <Enter> key to continue.");  
       in.readLine();  
     }  
      // System read requires this catch.  
      catch( java.io.IOException je)  
      {  
         PrintIOError(je);  
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
  
   // PrintIOError Function  
  
   static void PrintIOError( java.io.IOException je)  
   {  
      System.out.println("Error \n");  
      System.out.println("\tSource = " + je.getClass() + "\n");  
      System.out.println("\tDescription = " + je.getMessage() + "\n");  
   }  
}  
// EndErrorExampleVJ  
```