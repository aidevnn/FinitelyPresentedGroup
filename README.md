# FinitelyPresentedGroup
Bruteforce algorithm for creating all elements of a group presented by generators and relations. This current version runs very very slow even for smallest groups.

(In Progress)

``` 
WordStructureExt.Generate("a4", "a2b-2", "b-1aba"); // H8
```

will produce

```
G = { (), a, b, a-1, b-1, a2, ab, ab-1 }
|G| = 8

()   => ( 0: 0) 
a    => ( 1: 1) a
b    => ( 1: 1) b
a-1  => ( 1: 1) A
b-1  => ( 1: 1) B
a2   => ( 1: 2) aa
ab   => ( 2: 2) ab
ab-1 => ( 2: 2) aB

() |  a  b  A  B aa ab aB
-------------------------
 a | aa ab () aB  A  B  b
 b | aB aa ab ()  B  a  A
 A | () aB aa ab  a  b  B
 B | ab () aB aa  b  A  a
aa |  A  B  a  b () aB ab
ab |  b  A  B  a aB aa ()
aB |  B  a  b  A ab () aa

Repr : ()
    ()         => ( 0: 0) 
    a4         => ( 1: 4) aaaa
    a-4        => ( 1: 4) AAAA
    a2b2       => ( 2: 4) aabb
    a2b-2      => ( 2: 4) aaBB
    b2a2       => ( 2: 4) bbaa
    b-2a2      => ( 2: 4) BBaa
    ab-2a      => ( 3: 4) aBBa
    ba2b       => ( 3: 4) baab
    a-1b2a-1   => ( 3: 4) AbbA
    b-1a2b-1   => ( 3: 4) BaaB
    abab-1     => ( 4: 4) abaB
    ab-1a-1b-1 => ( 4: 4) aBAB
    baba-1     => ( 4: 4) babA
    ba-1b-1a-1 => ( 4: 4) bABA
Repr : a
    a       => ( 1: 1) a
    a-3     => ( 1: 3) AAA
    a-1b2   => ( 2: 3) Abb
    b2a-1   => ( 2: 3) bbA
    ba-1b-1 => ( 3: 3) bAB
Repr : b
    b       => ( 1: 1) b
    b-3     => ( 1: 3) BBB
    b-1a2   => ( 2: 3) Baa
    a2b-1   => ( 2: 3) aaB
    aba     => ( 3: 3) aba
    ab-1a-1 => ( 3: 3) aBA
Repr : a-1
    a-1   => ( 1: 1) A
    a3    => ( 1: 3) aaa
    ab-2  => ( 2: 3) aBB
    b-2a  => ( 2: 3) BBa
    bab-1 => ( 3: 3) baB
Repr : b-1
    b-1       => ( 1: 1) B
    b3        => ( 1: 3) bbb
    ba2       => ( 2: 3) baa
    a2b       => ( 2: 3) aab
    aba-1     => ( 3: 3) abA
    a-1b-1a-1 => ( 3: 3) ABA
Repr : a2
    a2  => ( 1: 2) aa
    b2  => ( 1: 2) bb
    a-2 => ( 1: 2) AA
    b-2 => ( 1: 2) BB
Repr : ab
    ab     => ( 2: 2) ab
    ba-1   => ( 2: 2) bA
    a-1b-1 => ( 2: 2) AB
    b-1a   => ( 2: 2) Ba
    b3a    => ( 2: 4) bbba
    b-3a-1 => ( 2: 4) BBBA
Repr : ab-1
    ab-1   => ( 2: 2) aB
    ba     => ( 2: 2) ba
    a-1b   => ( 2: 2) Ab
    b-1a-1 => ( 2: 2) BA
    ab3    => ( 2: 4) abbb

Total Time : 7197 ms

```