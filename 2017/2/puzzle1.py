import csv

with open("2017/2/input.csv", "r") as csvfile:
    reader = csv.reader(csvfile, delimiter="\t")
    rows = [[int(i) for i in row] for row in reader]
    print sum(map(lambda row: max(row) - min(row), rows))
