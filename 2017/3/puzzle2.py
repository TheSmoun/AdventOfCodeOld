import math

number = 289326
numbers = {1: 1, 15: 133}

class Square:

    def __init__(self, number):
        self.n = number
    
    def get_corners(self):
        print(self.n)
        if self.n == 1:
            yield 1
        
        for i in range(-4, 1):
            yield self.n * self.n + i * (self.n - 1)

print(list(Square(3).get_corners()))

"""
147  142  133  122   59
304    5    4    2   57
330   10    1    1   54
351   11   23   25   26
362  747  806--->   ...

17  16  15  14  13
18   5   4   3  12
19   6   1   2  11
20   7   8   9  10
21  22  23---> ...
"""