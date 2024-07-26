

function DataModel() {
    var self = this;
    self.users = ko.observableArray([]);
    self.roles = ko.observableArray([]);
    self.userRoles = ko.observableArray([]);
    self.selectedUser = ko.observable();
    self.selectedRole = ko.observable();
}

function ViewModel() {
    var self = this;

    self.dataModel = new DataModel();
    self.newUserName = ko.observable("");
    self.newRoleName = ko.observable("");

    self.fetchUsers = function () {
        fetch('http://localhost:5248/api/user')
            .then(response => response.json())
            .then(data => self.dataModel.users(data));
    };

    self.fetchRoles = function () {
        fetch('http://localhost:5248/api/role')
            .then(response => response.json())
            .then(data => self.dataModel.roles(data));
    };

    self.fetchUserRoles = function () {
        fetch('http://localhost:5248/api/userrole')
            .then(response => response.json())
            .then(data => {
                console.log("Fetched user roles:", data);
                self.dataModel.userRoles(data);
            })
            .catch(error => console.error('Error fetching user roles:', error));
    };

    self.canAddUserRole = ko.computed(function () {
        console.log("its working my g")
        return self.dataModel.selectedUser() && self.dataModel.selectedRole();
    });
    self.addUser = function () {
        if (self.newUserName()) {
            fetch('http://localhost:5248/api/user', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ name: self.newUserName() })
            })
                .then(response => response.json())
                .then(data => {
                    self.dataModel.users.push(data);
                    self.newUserName("");
                })
                .catch((error) => {
                    console.error('Error:', error);
                });
        }
    };

    self.addRole = function () {
        if (self.newRoleName()) {
            fetch('http://localhost:5248/api/role', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ name: self.newRoleName() })
            })
                .then(response => response.json())
                .then(data => {
                    self.dataModel.roles.push(data);
                    self.newRoleName("");
                })
                .catch((error) => {
                    console.error('Error:', error);
                });
        }
    };
    self.addUserRole = function () {
        if (self.canAddUserRole()) {
            var newUserRole = {
                userId: self.dataModel.selectedUser(),
                roleId: self.dataModel.selectedRole()
            };
            console.log(JSON.stringify(newUserRole));

            // Send POST request to backend
            fetch('http://localhost:5248/api/userrole', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newUserRole)
            })
                .then(response => {
                    if (!response.ok) {
                        throw new Error(`HTTP error! status: ${response.status}`);
                    }
                    return response.json();
                })
                .then(data => {
                    console.log("User role added successfully:", data);
                    // Fetch the updated user roles instead of pushing directly
                    self.fetchUserRoles();
                    // Reset selections
                    self.dataModel.selectedUser(null);
                    self.dataModel.selectedRole(null);
                })
                .catch((error) => {
                    console.error('Error adding user role:', error);
                });
        }
        
              };

    // Initialize data
    self.fetchUsers();
    self.fetchRoles();
    self.fetchUserRoles();
}

ko.applyBindings(new ViewModel());