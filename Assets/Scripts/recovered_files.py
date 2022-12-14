import os

with open("message.txt", "r") as file:
  cont = 1
  for line in file:
    list = line.split()
    print(list[-1])
    os.system('git show ' + list[-1] + ' > restored_file_'+ str(cont) +'.txt')
    cont+=1