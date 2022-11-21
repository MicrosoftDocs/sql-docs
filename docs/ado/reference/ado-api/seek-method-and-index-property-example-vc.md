---
title: "Seek Method and Index Property Example (VC++)"
description: "Seek Method and Index Property Example (VC++)"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "Seek method [ADO], VC++ example"
  - "index property [ADO]"
dev_langs:
  - "C++"
---
# Seek Method and Index Property Example (VC++)
This example uses the [Recordset](./recordset-object-ado.md) object's [Seek](./seek-method.md) method and [Index](./index-property.md) property in conjunction with a given ***Employee ID***, to locate the employee's name in the ***Employees*** table of the Nwind.mdb database.  
  
```  
// BeginSeekCpp.cpp  
// compile with: /EHsc  
#import "msado15.dll" no_namespace rename("EOF", "EndOfFile")  
  
#include <stdio.h>  
#include <ole2.h>  
#include <conio.h>  
#include <string.h>  
#include "icrsint.h"  
  
// Class extracts only EmployeeId,FirstName and LastName from employees table  
class CEmployeeRs : public CADORecordBinding {  
   BEGIN_ADO_BINDING(CEmployeeRs)  
  
   // Column hiredate is the 1st field in the table.  
   ADO_VARIABLE_LENGTH_ENTRY2(1, adInteger,m_ie_empid, sizeof(m_ie_empid),   
                              le_empidStatus, FALSE)  
  
   // Column LastName is the 2nd field in the table.  
   ADO_VARIABLE_LENGTH_ENTRY2(2, adVarChar, m_sze_lname, sizeof(m_sze_lname),   
                             le_lnameStatus, FALSE)  
  
   // Column FirstName is the 3rd field in the table.  
   ADO_VARIABLE_LENGTH_ENTRY2(2, adVarChar, m_sze_fname, sizeof(m_sze_fname),   
                             le_fnameStatus, FALSE)  
   END_ADO_BINDING()  
  
public:  
   INT m_ie_empid;  
   ULONG le_empidStatus;  
   CHAR m_sze_fname[11];  
   ULONG le_fnameStatus;  
   CHAR m_sze_lname[21];  
   ULONG le_lnameStatus;      
};  
  
// Function declarations  
inline void TESTHR(HRESULT x) { if FAILED(x) _com_issue_error(x); };  
void SeekX();  
void PrintProviderError(_ConnectionPtr pConnection);  
void PrintComError(_com_error &e);  
  
inline char* mygets(char* strDest, int n) {      
   char strExBuff[10];  
   char* pstrRet = fgets(strDest, n, stdin);  
  
   if (pstrRet == NULL)  
      return NULL;  
  
   if (!strrchr(strDest, '\n'))  
      // Exhaust the input buffer.  
      do {  
         fgets(strExBuff, sizeof(strExBuff), stdin);  
      } while (!strrchr(strExBuff, '\n'));  
   else  
      // Replace '\n' with '\0'  
      strDest[strrchr(strDest, '\n') - strDest] = '\0';  
  
   return pstrRet;  
}  
  
int main() {  
   if (FAILED(::CoInitialize(NULL)))  
      return -1;  
  
   SeekX();  
   ::CoUninitialize();  
}  
  
void SeekX() {  
   HRESULT hr = S_OK;  
  
   // Define ADO object pointers, initialize pointers on define. These are in the ADODB::  namespace.  
   _RecordsetPtr pRstEmp = NULL;  
  
   IADORecordBinding *picRs = NULL;   // Interface Pointer declared  
   CEmployeeRs EmpRs;   // C++ class object  
  
   // Definitions of other variables  
   _bstr_t strPrompt("Enter an EmployeeID (e.g., 1 to 9)");  
   char strEmpId[2];  
  
   try {  
      TESTHR(pRstEmp.CreateInstance(__uuidof(Recordset)));  
      pRstEmp->CursorLocation = adUseServer;  
      pRstEmp->Open("employees", "Provider='Microsoft.Jet.OLEDB.4.0';"   
         "Data Source='C:\\Program Files\\Microsoft Office\\Office\\"  
         "Samples\\Northwind.mdb';",  
         adOpenKeyset,adLockReadOnly,adCmdTableDirect);  
  
      // Open an IADORecordBinding interface pointer which   
      // we'll use for binding Recordset to a Class    
      TESTHR(pRstEmp->QueryInterface(__uuidof(IADORecordBinding), (LPVOID*)&picRs));  
  
      // Bind the Recordset to a C++ Class  
      TESTHR(picRs->BindToRecordset(&EmpRs));  
  
      // Does this provider support Seek and Index?  
      if (pRstEmp->Supports(adIndex) && pRstEmp->Supports(adSeek)) {  
         pRstEmp->Index = "PrimaryKey";  
  
         // Display all the employees.  
         pRstEmp->MoveFirst();  
         while (!pRstEmp->EndOfFile) {  
            printf("%d : %s %s\n",    
               EmpRs.le_empidStatus == adFldOK ?   
               EmpRs.m_ie_empid : 0,   
               EmpRs.le_fnameStatus == adFldOK ?   
               EmpRs.m_sze_fname : "<NULL>",   
               EmpRs.le_lnameStatus == adFldOK ?   
               EmpRs.m_sze_lname : "<NULL>");  
  
            pRstEmp->MoveNext();  
         }  
         // Prompt the user for an EmployeeID between 1 and 9.  
         do {  
            pRstEmp->MoveFirst();  
            printf("\n\n%s\t",(LPCSTR) strPrompt);  
            mygets(strEmpId, 2);  
  
            // Quit if strEmpID is a zero-length string (CANCEL, null, etc.)  
            char *strTemp = strtok_s(strEmpId," \t", NULL);  
            if (strTemp == NULL) break;  
            if (strlen(strTemp) == 1 && atoi(strTemp) >= 1 && atoi(strTemp) <= 9) {  
               _variant_t strEmployeeId(strTemp);  
               pRstEmp->Seek(strEmployeeId, adSeekAfterEQ);  
               if (pRstEmp->EndOfFile)  
                  printf("Employee not found.\n");  
               else {  
                  printf("%d : Employee='%s %s'\n",    
                     EmpRs.le_empidStatus == adFldOK ?   
                     EmpRs.m_ie_empid : 0,  
                     EmpRs.le_fnameStatus == adFldOK ?   
                     EmpRs.m_sze_fname : "<NULL>",   
                     EmpRs.le_lnameStatus == adFldOK ?   
                     EmpRs.m_sze_lname : "<NULL>");  
               }  
            }  
         } while(true);  
      }    
   }  
   catch(_com_error &e) {  
      // Notify the user of errors if any.  
      // Pass a connection pointer accessed from the Recordset.  
      _variant_t vtConnect = pRstEmp->GetActiveConnection();  
  
      // GetActiveConnection returns connect string if connection  
      // is not open, else returns Connection object.  
      switch(vtConnect.vt) {  
         case VT_BSTR:  
            PrintComError(e);  
            break;  
         case VT_DISPATCH:  
            PrintProviderError(vtConnect);  
            break;  
         default:  
            printf("Errors occured.");  
            break;  
      }  
   }  
  
   // Clean up objects before exit.  Release the IADORecordset Interface here     
   if (picRs)  
      picRs->Release();  
  
   if (pRstEmp)  
      if (pRstEmp->State == adStateOpen)  
         pRstEmp->Close();  
}  
  
void PrintProviderError(_ConnectionPtr pConnection) {  
   // Print Provider Errors from Connection object.  
   // pErr is a record object in the Connection's Error collection.  
   ErrorPtr pErr = NULL;  
  
   if ( (pConnection->Errors->Count) > 0) {  
      long nCount = pConnection->Errors->Count;  
  
      // Collection ranges from 0 to nCount -1.  
      for (long i = 0 ; i < nCount ; i++) {  
         pErr = pConnection->Errors->GetItem(i);  
         printf("\t Error number: %x\t%s", pErr->Number, pErr->Description);  
      }  
   }  
}  
  
void PrintComError(_com_error &e) {  
   _bstr_t bstrSource(e.Source());  
   _bstr_t bstrDescription(e.Description());  
  
   // Print COM errors.   
   printf("Error\n");  
   printf("\tCode = %08lx\n", e.Error());  
   printf("\tCode meaning = %s\n", e.ErrorMessage());  
   printf("\tSource = %s\n", (LPCSTR) bstrSource);  
   printf("\tDescription = %s\n", (LPCSTR) bstrDescription);  
}  
```  
  
## See Also  
 [Index Property](./index-property.md)   
 [Recordset Object (ADO)](./recordset-object-ado.md)   
 [Seek Method](./seek-method.md)