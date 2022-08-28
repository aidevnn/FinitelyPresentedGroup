# FinitelyPresentedGroup
Bruteforce algorithm for creating all elements of a group presented by generators and relations. This current version runs very very slow even for smallest groups.
``` 
Generate("a2", "b2", "ababab"); // S3

Generate("a2", "b2", "abab"); // Klein

Generate("a3", "b2", "aba-1b-1"); // C6

Generate("a4", "a2b-2", "b-1aba"); // H8

Generate("a3", "b6", "ab = ba"); // C3 x C6 

Generate("a2", "b2", "c2", "bcbcbc", "acacac", "abab"); // S4

```

will produce
```
G = { (), a, b, ab, ba, aba }
()  => ( 0: 0) 
a   => ( 1: 1) a
b   => ( 1: 1) b
ab  => ( 2: 2) ab
ba  => ( 2: 2) ba
aba => ( 3: 3) aba

Order = 6
Is Group   : True
Is Abelian : False

 () |   a   b  ab  ba aba
-------------------------
  a |  ()  ab   b aba  ba
  b |  ba  () aba   a  ab
 ab | aba   a  ba  ()   b
 ba |   b aba  ()  ab   a
aba |  ab  ba   a   b  ()

Repr : ()
    ()     => ( 0: 0) 
    a2     => ( 1: 2) aa
    b2     => ( 1: 2) bb
    a-2    => ( 1: 2) AA
    b-2    => ( 1: 2) BB
    ababab => ( 6: 6) ababab
    bababa => ( 6: 6) bababa
Repr : a
    a     => ( 1: 1) a
    a-1   => ( 1: 1) A
    babab => ( 5: 5) babab
Repr : b
    b     => ( 1: 1) b
    b-1   => ( 1: 1) B
    ababa => ( 5: 5) ababa
Repr : ab
    ab   => ( 2: 2) ab
    baba => ( 4: 4) baba
Repr : ba
    ba   => ( 2: 2) ba
    abab => ( 4: 4) abab
Repr : aba
    aba => ( 3: 3) aba
    bab => ( 3: 3) bab

Total Words : 19
Total Time  : 203 ms; Total Created Words : 3256
```
and
```


G = { (), a, b, ab }
() => ( 0: 0) 
a  => ( 1: 1) a
b  => ( 1: 1) b
ab => ( 2: 2) ab

Order = 4
Is Group   : True
Is Abelian : True

() |  a  b ab
-------------
 a | () ab  b
 b | ab ()  a
ab |  b  a ()

Repr : ()
    ()   => ( 0: 0) 
    a2   => ( 1: 2) aa
    b2   => ( 1: 2) bb
    a-2  => ( 1: 2) AA
    b-2  => ( 1: 2) BB
    abab => ( 4: 4) abab
    baba => ( 4: 4) baba
Repr : a
    a   => ( 1: 1) a
    a-1 => ( 1: 1) A
    bab => ( 3: 3) bab
Repr : b
    b   => ( 1: 1) b
    b-1 => ( 1: 1) B
    aba => ( 3: 3) aba
Repr : ab
    ab => ( 2: 2) ab
    ba => ( 2: 2) ba

Total Words : 15
Total Time  : 54 ms; Total Created Words : 1813
```
and
```
G = { (), a, b, a-1, ab, ba-1 }
()   => ( 0: 0) 
a    => ( 1: 1) a
b    => ( 1: 1) b
a-1  => ( 1: 1) A
ab   => ( 2: 2) ab
ba-1 => ( 2: 2) bA

Order = 6
Is Group   : True
Is Abelian : True

() |  a  b  A ab bA
-------------------
 a |  A ab () bA  b
 b | ab () bA  a  A
 A | () bA  a  b ab
ab | bA  a  b  A ()
bA |  b  A ab ()  a

Repr : ()
    ()     => ( 0: 0) 
    b2     => ( 1: 2) bb
    b-2    => ( 1: 2) BB
    a3     => ( 1: 3) aaa
    a-3    => ( 1: 3) AAA
    aba-1b => ( 4: 4) abAb
    baba-1 => ( 4: 4) babA
    ba-1ba => ( 4: 4) bAba
    a-1bab => ( 4: 4) Abab
Repr : a
    a   => ( 1: 1) a
    a-2 => ( 1: 2) AA
    bab => ( 3: 3) bab
Repr : b
    b     => ( 1: 1) b
    b-1   => ( 1: 1) B
    aba-1 => ( 3: 3) abA
    a-1ba => ( 3: 3) Aba
Repr : a-1
    a-1   => ( 1: 1) A
    a2    => ( 1: 2) aa
    ba-1b => ( 3: 3) bAb
Repr : ab
    ab => ( 2: 2) ab
    ba => ( 2: 2) ba
Repr : ba-1
    ba-1 => ( 2: 2) bA
    a-1b => ( 2: 2) Ab

Total Words : 23
Total Time  : 183 ms; Total Created Words : 3949
```
and
```
G = { (), a, b, a-1, b-1, a2, ab, ab-1 }
()   => ( 0: 0) 
a    => ( 1: 1) a
b    => ( 1: 1) b
a-1  => ( 1: 1) A
b-1  => ( 1: 1) B
a2   => ( 1: 2) aa
ab   => ( 2: 2) ab
ab-1 => ( 2: 2) aB

Order = 8
Is Group   : True
Is Abelian : False

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

Total Words : 52
Total Time  : 3174 ms; Total Created Words : 36396
```
and
```
G = { (), a, b, a-1, b-1, b2, b-2, b3, ab, ab-1, ba-1, a-1b-1, ab2, ab-2, a-1b2, a-1b-2, ab3, a-1b3 }
()     => ( 0: 0) 
a      => ( 1: 1) a
b      => ( 1: 1) b
a-1    => ( 1: 1) A
b-1    => ( 1: 1) B
b2     => ( 1: 2) bb
b-2    => ( 1: 2) BB
b3     => ( 1: 3) bbb
ab     => ( 2: 2) ab
ab-1   => ( 2: 2) aB
ba-1   => ( 2: 2) bA
a-1b-1 => ( 2: 2) AB
ab2    => ( 2: 3) abb
ab-2   => ( 2: 3) aBB
a-1b2  => ( 2: 3) Abb
a-1b-2 => ( 2: 3) ABB
ab3    => ( 2: 4) abbb
a-1b3  => ( 2: 4) Abbb

Order = 18
Is Group   : True
Is Abelian : True

  () |    a    b    A    B   bb   BB  bbb   ab   aB   bA   AB  abb  aBB  Abb  ABB abbb Abbb
-------------------------------------------------------------------------------------------
   a |    A   ab   ()   aB  abb  aBB abbb   bA   AB    b    B  Abb  ABB   bb   BB Abbb  bbb
   b |   ab   bb   bA   ()  bbb    B   BB  abb    a  Abb    A abbb   aB Abbb   AB  aBB  ABB
   A |   ()   bA    a   AB  Abb  ABB Abbb    b    B   ab   aB   bb   BB  abb  aBB  bbb abbb
   B |   aB   ()   AB   BB    b  bbb   bb    a  aBB    A  ABB   ab abbb   bA Abbb  abb  Abb
  bb |  abb  bbb  Abb    b   BB   ()    B abbb   ab Abbb   bA  aBB    a  ABB    A   aB   AB
  BB |  aBB    B  ABB  bbb   ()   bb    b   aB abbb   AB Abbb    a  abb    A  Abb   ab   bA
 bbb | abbb   BB Abbb   bb    B    b   ()  aBB  abb  ABB  Abb   aB   ab   AB   bA    a    A
  ab |   bA  abb    b    a abbb   aB  aBB  Abb    A   bb   () Abbb   AB  bbb    B  ABB   BB
  aB |   AB    a    B  aBB   ab abbb  abb    A  ABB   ()   BB   bA Abbb    b  bbb  Abb   bb
  bA |    b  Abb   ab    A Abbb   AB  ABB   bb   ()  abb    a  bbb    B abbb   aB   BB  aBB
  AB |    B    A   aB  ABB   bA Abbb  Abb   ()   BB    a  aBB    b  bbb   ab abbb   bb  abb
 abb |  Abb abbb   bb   ab  aBB    a   aB Abbb   bA  bbb    b  ABB    A   BB   ()   AB    B
 aBB |  ABB   aB   BB abbb    a  abb   ab   AB Abbb    B  bbb    A  Abb   ()   bb   bA    b
 Abb |   bb Abbb  abb   bA  ABB    A   AB  bbb    b abbb   ab   BB   ()  aBB    a    B   aB
 ABB |   BB   AB  aBB Abbb    A  Abb   bA    B  bbb   aB abbb   ()   bb    a  abb    b   ab
abbb | Abbb  aBB  bbb  abb   aB   ab    a  ABB  Abb   BB   bb   AB   bA    B    b    A   ()
Abbb |  bbb  ABB abbb  Abb   AB   bA    A   BB   bb  aBB  abb    B    b   aB   ab   ()    a

Repr : ()
    ()       => ( 0: 0) 
    a3       => ( 1: 3) aaa
    a-3      => ( 1: 3) AAA
    b6       => ( 1: 6) bbbbbb
    b-6      => ( 1: 6) BBBBBB
    aba-1b-1 => ( 4: 4) abAB
    ab-1a-1b => ( 4: 4) aBAb
    bab-1a-1 => ( 4: 4) baBA
    ba-1b-1a => ( 4: 4) bABa
    a-1bab-1 => ( 4: 4) AbaB
    a-1b-1ab => ( 4: 4) ABab
    b-1aba-1 => ( 4: 4) BabA
    b-1a-1ba => ( 4: 4) BAba
Repr : a
    a     => ( 1: 1) a
    a-2   => ( 1: 2) AA
    bab-1 => ( 3: 3) baB
    b-1ab => ( 3: 3) Bab
Repr : b
    b     => ( 1: 1) b
    b-5   => ( 1: 5) BBBBB
    aba-1 => ( 3: 3) abA
    a-1ba => ( 3: 3) Aba
Repr : a-1
    a-1     => ( 1: 1) A
    a2      => ( 1: 2) aa
    ba-1b-1 => ( 3: 3) bAB
    b-1a-1b => ( 3: 3) BAb
Repr : b-1
    b-1     => ( 1: 1) B
    b5      => ( 1: 5) bbbbb
    ab-1a-1 => ( 3: 3) aBA
    a-1b-1a => ( 3: 3) ABa
Repr : b2
    b2     => ( 1: 2) bb
    b-4    => ( 1: 4) BBBB
    ab2a-1 => ( 3: 4) abbA
    a-1b2a => ( 3: 4) Abba
Repr : b-2
    b-2     => ( 1: 2) BB
    b4      => ( 1: 4) bbbb
    ab-2a-1 => ( 3: 4) aBBA
    a-1b-2a => ( 3: 4) ABBa
Repr : b3
    b3  => ( 1: 3) bbb
    b-3 => ( 1: 3) BBB
Repr : ab
    ab => ( 2: 2) ab
    ba => ( 2: 2) ba
Repr : ab-1
    ab-1 => ( 2: 2) aB
    b-1a => ( 2: 2) Ba
Repr : ba-1
    ba-1 => ( 2: 2) bA
    a-1b => ( 2: 2) Ab
Repr : a-1b-1
    a-1b-1 => ( 2: 2) AB
    b-1a-1 => ( 2: 2) BA
Repr : ab2
    ab2      => ( 2: 3) abb
    b2a      => ( 2: 3) bba
    a-1b2a-1 => ( 3: 4) AbbA
Repr : ab-2
    ab-2      => ( 2: 3) aBB
    b-2a      => ( 2: 3) BBa
    a-1b-2a-1 => ( 3: 4) ABBA
Repr : a-1b2
    a-1b2 => ( 2: 3) Abb
    b2a-1 => ( 2: 3) bbA
    ab2a  => ( 3: 4) abba
Repr : a-1b-2
    a-1b-2 => ( 2: 3) ABB
    b-2a-1 => ( 2: 3) BBA
    ab-2a  => ( 3: 4) aBBa
    ba-1b3 => ( 3: 5) bAbbb
Repr : ab3
    ab3 => ( 2: 4) abbb
    b3a => ( 2: 4) bbba
Repr : a-1b3
    a-1b3  => ( 2: 4) Abbb
    b3a-1  => ( 2: 4) bbbA
    ba-1b2 => ( 3: 4) bAbb

Total Words : 65
Total Time  : 4666 ms; Total Created Words : 44573
```
and
```
G = { (), a, b, c, ab, ac, bc, ca, cb, abc, aca, acb, bca, bcb, cab, abca, abcb, acab, bcab, cabc, abcab, acabc, bcabc, abcabc }
()     => ( 0: 0) 
a      => ( 1: 1) a
b      => ( 1: 1) b
c      => ( 1: 1) c
ab     => ( 2: 2) ab
ac     => ( 2: 2) ac
bc     => ( 2: 2) bc
ca     => ( 2: 2) ca
cb     => ( 2: 2) cb
abc    => ( 3: 3) abc
aca    => ( 3: 3) aca
acb    => ( 3: 3) acb
bca    => ( 3: 3) bca
bcb    => ( 3: 3) bcb
cab    => ( 3: 3) cab
abca   => ( 4: 4) abca
abcb   => ( 4: 4) abcb
acab   => ( 4: 4) acab
bcab   => ( 4: 4) bcab
cabc   => ( 4: 4) cabc
abcab  => ( 5: 5) abcab
acabc  => ( 5: 5) acabc
bcabc  => ( 5: 5) bcabc
abcabc => ( 6: 6) abcabc

Order = 24
Is Group   : True
Is Abelian : False

    () |      a      b      c     ab     ac     bc     ca     cb    abc    aca    acb    bca    bcb    cab   abca   abcb   acab   bcab   cabc  abcab  acabc  bcabc abcabc
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
     a |     ()     ab     ac      b      c    abc    aca    acb     bc     ca     cb   abca   abcb   acab    bca    bcb    cab  abcab  acabc   bcab   cabc abcabc  bcabc
     b |     ab     ()     bc      a    abc      c    bca    bcb     ac   abca   abcb     ca     cb   bcab    aca    acb  abcab    cab  bcabc   acab abcabc   cabc  acabc
     c |     ca     cb     ()    cab    aca    bcb      a      b   cabc     ac   acab   bcab     bc     ab  bcabc  acabc    acb    bca    abc abcabc   abcb   abca  abcab
    ab |      b      a    abc     ()     bc     ac   abca   abcb      c    bca    bcb    aca    acb  abcab     ca     cb   bcab   acab abcabc    cab  bcabc  acabc   cabc
    ac |    aca    acb      a   acab     ca   abcb     ()     ab  acabc      c    cab  abcab    abc      b abcabc   cabc     cb   abca     bc  bcabc    bcb    bca   bcab
    bc |    bca    bcb      b   bcab   abca     cb     ab     ()  bcabc    abc  abcab    cab      c      a   cabc abcabc   abcb     ca     ac  acabc    acb    aca   acab
    ca |      c    cab    aca     cb     ()   cabc     ac   acab    bcb      a      b  bcabc  acabc    acb   bcab     bc     ab abcabc   abcb    bca    abc  abcab   abca
    cb |    cab      c    bcb     ca   cabc     ()   bcab     bc    aca  bcabc  acabc      a      b    bca     ac   acab abcabc     ab   abca    acb  abcab    abc   abcb
   abc |   abca   abcb     ab  abcab    bca    acb      b      a abcabc     bc   bcab   acab     ac     ()  acabc  bcabc    bcb    aca      c   cabc     cb     ca    cab
   aca |     ac   acab     ca    acb      a  acabc      c    cab   abcb     ()     ab abcabc   cabc     cb  abcab    abc      b  bcabc    bcb   abca     bc   bcab    bca
   acb |   acab     ac   abcb    aca  acabc      a  abcab    abc     ca abcabc   cabc     ()     ab   abca      c    cab  bcabc      b    bca     cb   bcab     bc    bcb
   bca |     bc   bcab   abca    bcb      b  bcabc    abc  abcab     cb     ab     ()   cabc abcabc   abcb    cab      c      a  acabc    acb     ca     ac   acab    aca
   bcb |   bcab     bc     cb    bca  bcabc      b    cab      c   abca   cabc abcabc     ab     ()     ca    abc  abcab  acabc      a    aca   abcb   acab     ac    acb
   cab |     cb     ca   cabc      c    bcb    aca  bcabc  acabc     ()   bcab     bc     ac   acab abcabc      a      b    bca    acb  abcab     ab   abca   abcb    abc
  abca |    abc  abcab    bca   abcb     ab abcabc     bc   bcab    acb      b      a  acabc  bcabc    bcb   acab     ac     ()   cabc     cb    aca      c    cab     ca
  abcb |  abcab    abc    acb   abca abcabc     ab   acab     ac    bca  acabc  bcabc      b      a    aca     bc   bcab   cabc     ()     ca    bcb    cab      c     cb
  acab |    acb    aca  acabc     ac   abcb     ca abcabc   cabc      a  abcab    abc      c    cab  bcabc     ()     ab   abca     cb   bcab      b    bca    bcb     bc
  bcab |    bcb    bca  bcabc     bc     cb   abca   cabc abcabc      b    cab      c    abc  abcab  acabc     ab     ()     ca   abcb   acab      a    aca    acb     ac
  cabc |  bcabc  acabc    cab abcabc   bcab   acab     cb     ca  abcab    bcb    bca    acb    aca      c   abcb   abca     bc     ac     ()    abc      b      a     ab
 abcab |   abcb   abca abcabc    abc    acb    bca  acabc  bcabc     ab   acab     ac     bc   bcab   cabc      b      a    aca    bcb    cab     ()     ca     cb      c
 acabc | abcabc   cabc   acab  bcabc  abcab    cab    acb    aca   bcab   abcb   abca     cb     ca     ac    bcb    bca    abc      c      a     bc     ab     ()      b
 bcabc |   cabc abcabc   bcab  acabc    cab  abcab    bcb    bca   acab     cb     ca   abcb   abca     bc    acb    aca      c    abc      b     ac     ()     ab      a
abcabc |  acabc  bcabc  abcab   cabc   acab   bcab   abcb   abca    cab    acb    aca    bcb    bca    abc     cb     ca     ac     bc     ab      c      a      b     ()

Repr : ()
    ()     => ( 0: 0) 
    a2     => ( 1: 2) aa
    b2     => ( 1: 2) bb
    c2     => ( 1: 2) cc
    a-2    => ( 1: 2) AA
    b-2    => ( 1: 2) BB
    c-2    => ( 1: 2) CC
    abab   => ( 4: 4) abab
    baba   => ( 4: 4) baba
    acacac => ( 6: 6) acacac
    bcbcbc => ( 6: 6) bcbcbc
    cacaca => ( 6: 6) cacaca
    cbcbcb => ( 6: 6) cbcbcb
Repr : a
    a     => ( 1: 1) a
    a-1   => ( 1: 1) A
    bab   => ( 3: 3) bab
    cacac => ( 5: 5) cacac
Repr : b
    b     => ( 1: 1) b
    b-1   => ( 1: 1) B
    aba   => ( 3: 3) aba
    cbcbc => ( 5: 5) cbcbc
Repr : c
    c     => ( 1: 1) c
    c-1   => ( 1: 1) C
    acaca => ( 5: 5) acaca
    bcbcb => ( 5: 5) bcbcb
Repr : ab
    ab => ( 2: 2) ab
    ba => ( 2: 2) ba
Repr : ac
    ac   => ( 2: 2) ac
    caca => ( 4: 4) caca
Repr : bc
    bc   => ( 2: 2) bc
    cbcb => ( 4: 4) cbcb
Repr : ca
    ca   => ( 2: 2) ca
    acac => ( 4: 4) acac
Repr : cb
    cb   => ( 2: 2) cb
    bcbc => ( 4: 4) bcbc
Repr : abc
    abc => ( 3: 3) abc
Repr : aca
    aca => ( 3: 3) aca
    cac => ( 3: 3) cac
Repr : acb
    acb => ( 3: 3) acb
Repr : bca
    bca => ( 3: 3) bca
Repr : bcb
    bcb => ( 3: 3) bcb
    cbc => ( 3: 3) cbc
Repr : cab
    cab => ( 3: 3) cab
Repr : abca
    abca => ( 4: 4) abca
Repr : abcb
    abcb => ( 4: 4) abcb
Repr : acab
    acab => ( 4: 4) acab
Repr : bcab
    bcab => ( 4: 4) bcab
Repr : cabc
    cabc   => ( 4: 4) cabc
    acabcb => ( 6: 6) acabcb
    bcabca => ( 6: 6) bcabca
Repr : abcab
    abcab => ( 5: 5) abcab
Repr : acabc
    acabc => ( 5: 5) acabc
    cabcb => ( 5: 5) cabcb
Repr : bcabc
    bcabc => ( 5: 5) bcabc
    cabca => ( 5: 5) cabca
Repr : abcabc
    abcabc => ( 6: 6) abcabc

Total Words : 56
Total Time  : 8971 ms; Total Created Words : 100839
```