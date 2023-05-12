variables = {}

def print_variable_value(variable_name):
    print(variables.get(variable_name, "Невизначена змінна"))

def assign_variable(command):
    variable_name, variable_value = command.split("=")
    variables[variable_name.strip()] = eval(variable_value.strip(), {}, variables)

def execute_math_operation():
    expression = input("UPPL>>> ")
    tokens = expression.split(' ')
    result = 0
    operation = ""

    for token in tokens:
        if token.isnumeric():
            number = float(token)
            if operation == "+":
                result += number
            elif operation == "-":
                result -= number
            elif operation == "*":
                result *= number
            elif operation == "/":
                if number != 0:
                    result /= number
                else:
                    print("Ділення на нуль заборонено!")
                    break
            else:
                result = number
        else:
            operation = token

    print(result)


print("Вітаємо в UPPl Інтерпрітаторі!")

while True:
    command = input("UPPl>>> ")

    if command.startswith("Надрукувати"):
        variable_name = command.split()[1]
        print_variable_value(variable_name)

    elif "=" in command:
        assign_variable(command)

    elif command == "ввийти":
        break

    elif command == "М":
        execute_math_operation()

    elif command == "Допомога":
        print("Перелік команд:")
        print("Надрукувати змінну, Наприклад (Надрукувати x)")
        print("Також ти можеш зберігати змінну")
        print("Наприклад (x = 6)")
        print("Ти можеш робити математичні вирази, написав М")