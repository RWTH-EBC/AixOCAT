from board import board

# create board instance
print("Creating board instance..")
board = board()
print("The board instance is now available.")

def read_v_raw(board=board):
    f = open('v_in', 'w')
    
    j = 0
    while j < 11:
        for i in range(1000):
            s = str(board.read_voltage(read_V_flag="raw"))
            f.write(s+'\n')
        try:
            input("Press Enter to continue...")
        except SyntaxError:
            pass
        j = j+1
    f.close()
    
    f = open('v_in', 'r')
    f.read()
    f.close()

def write_v(board=board):
    
    j = 0
    while j < 21:
        board.write_voltage(j*0.5)
        try:
            input("Press Enter to continue...")
        except SyntaxError:
            pass
        j = j+1

def write_i(board=board):
    
    j = 0
    while j < 21:
        board.write_current(j)
        try:
            input("Press Enter to continue...")
        except SyntaxError:
            pass
        j = j+1
        