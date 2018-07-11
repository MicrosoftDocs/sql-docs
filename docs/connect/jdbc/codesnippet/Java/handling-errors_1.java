public static void executeSQLException(Connection con) {
   try {
      String SQL = "SELECT TOP 10 * Person.Contact";
      Statement stmt = con.createStatement();
      ResultSet rs = stmt.executeQuery(SQL);

      while (rs.next()) {
         System.out.println(rs.getString(4) + " " + rs.getString(6));
      }
      stmt.close();
   }
   catch (SQLException se) {
      do {
         System.out.println("SQL STATE: " + se.getSQLState());
         System.out.println("ERROR CODE: " + se.getErrorCode());
         System.out.println("MESSAGE: " + se.getMessage());
         System.out.println();
         se = se.getNextException();
      } while (se != null);
   }
}