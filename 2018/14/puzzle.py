recipe = "320851"
score = "37"
elfs = [0, 1]

while recipe not in score[-7:]:
    score += str(sum(int(score[elf]) for elf in elfs))
    elfs = [(elf + int(score[elf]) + 1) % len(score) for elf in elfs]
    
print(score[int(recipe):int(recipe) + 10])
print(score.index(recipe))
