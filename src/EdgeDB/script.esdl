start migration to {
  module default {

    type User {
      required property firstName -> str;
      required property lastName -> str;

      required property email -> str;
      required property password -> str;
      required property passwordSalt -> str;
      required property passwordHash -> str;

      link googleLogin -> GoogleAccount;
    };

    type GoogleAccount {
      required property profileId -> str;
      required property email -> str;
    };

    type Project {
      required property name -> str;

      multi link tests -> TestCase;
    };

    type TestCase {
      required property name -> str;
    };

  };
};

populate migration;
describe current migration;
commit migration;




start migration to {
  module default {

    type Project {
      required property name -> str;

      multi link tests -> TestCase;
    };

    type TestCase {
      required property name -> str {
        constraint exclusive;
      };
    };

  };
};
populate migration;
describe current migration;
commit migration;
