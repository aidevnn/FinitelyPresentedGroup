# FinitelyPresentedGroup
Bruteforce algorithm for creating all elements of a group presented by generators and relations. This current version runs very very slow even for smallest groups.

For example, it takes ~2000ms for generating Symm4 group of permutations.
``` 
WordGroup.Generate("a2", "b2", "abab"); // Klein

WordGroup.Generate("a3", "b2", "aba-1b-1"); // C6

WordGroup.Generate("a2", "b2", "ababab"); // S3

WordGroup.Generate("a4", "a2b-2", "b-1aba"); // H8

WordGroup.Generate("a3", "b6", "ab = ba"); // C3 x C6

WordGroup.Generate("a2", "b2", "c2", "bcbcbc", "acacac", "abab"); // S4
```

will produce
```
G = { (), a, b, ab }
() => ()
a  => a
b  => b
ab => ab

Order = 4
Is Group   : True
Is Abelian : True

() |  a  b ab
-------------
 a | () ab  b
 b | ab ()  a
ab |  b  a ()

Repr : ()
    ()   => ()
    a2   => aa
    b2   => bb
    a-2  => AA
    b-2  => BB
    abab => abab
    baba => baba
Repr : a
    a   => a
    a-1 => A
    bab => bab
Repr : b
    b   => b
    b-1 => B
    aba => aba
Repr : ab
    ab => ab
    ba => ba

Total Words : 15
Total Time  : 29 ms; Total Created Words : 1867
```
and
```
G = { (), a, b, a-1, ab, ba-1 }
()   => ()
a    => a
b    => b
a-1  => A
ab   => ab
ba-1 => bA

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
    ()     => ()
    b2     => bb
    b-2    => BB
    a3     => aaa
    a-3    => AAA
    aba-1b => abAb
    baba-1 => babA
    ba-1ba => bAba
    a-1bab => Abab
Repr : a
    a   => a
    a-2 => AA
    bab => bab
Repr : b
    b     => b
    b-1   => B
    aba-1 => abA
    a-1ba => Aba
Repr : A
    a-1   => A
    a2    => aa
    ba-1b => bAb
Repr : ab
    ab => ab
    ba => ba
Repr : bA
    ba-1 => bA
    a-1b => Ab

Total Words : 23
Total Time  : 71 ms; Total Created Words : 4001
```
and
```
G = { (), a, b, ab, ba, aba }
()  => ()
a   => a
b   => b
ab  => ab
ba  => ba
aba => aba

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
    ()     => ()
    a2     => aa
    b2     => bb
    a-2    => AA
    b-2    => BB
    ababab => ababab
    bababa => bababa
Repr : a
    a     => a
    a-1   => A
    babab => babab
Repr : b
    b     => b
    b-1   => B
    ababa => ababa
Repr : ab
    ab   => ab
    baba => baba
Repr : ba
    ba   => ba
    abab => abab
Repr : aba
    aba => aba
    bab => bab

Total Words : 19
Total Time  : 66 ms; Total Created Words : 3315
```
and
```
G = { (), a, b, a-1, b-1, a2, ab, ab-1 }
()   => ()
a    => a
b    => b
a-1  => A
b-1  => B
a2   => aa
ab   => ab
ab-1 => aB

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
    ()         => ()
    a4         => aaaa
    a-4        => AAAA
    a2b2       => aabb
    a2b-2      => aaBB
    b2a2       => bbaa
    b-2a2      => BBaa
    ab-2a      => aBBa
    ba2b       => baab
    a-1b2a-1   => AbbA
    b-1a2b-1   => BaaB
    abab-1     => abaB
    ab-1a-1b-1 => aBAB
    baba-1     => babA
    ba-1b-1a-1 => bABA
Repr : a
    a       => a
    a-3     => AAA
    a-1b2   => Abb
    b2a-1   => bbA
    ba-1b-1 => bAB
Repr : b
    b       => b
    b-3     => BBB
    b-1a2   => Baa
    a2b-1   => aaB
    aba     => aba
    ab-1a-1 => aBA
Repr : A
    a-1   => A
    a3    => aaa
    ab-2  => aBB
    b-2a  => BBa
    bab-1 => baB
Repr : B
    b-1       => B
    b3        => bbb
    ba2       => baa
    a2b       => aab
    aba-1     => abA
    a-1b-1a-1 => ABA
Repr : aa
    a2  => aa
    b2  => bb
    a-2 => AA
    b-2 => BB
Repr : ab
    ab     => ab
    ba-1   => bA
    a-1b-1 => AB
    b-1a   => Ba
    b3a    => bbba
    b-3a-1 => BBBA
Repr : aB
    ab-1   => aB
    ba     => ba
    a-1b   => Ab
    b-1a-1 => BA
    ab3    => abbb

Total Words : 52
Total Time  : 573 ms; Total Created Words : 34791
```
and
```
G = { (), a, b, a-1, b-1, b2, b-2, b3, ab, ab-1, ba-1, a-1b-1, ab2, ab-2, a-1b2, a-1b-2, ab3, a-1b3 }
()     => ()
a      => a
b      => b
a-1    => A
b-1    => B
b2     => bb
b-2    => BB
b3     => bbb
ab     => ab
ab-1   => aB
ba-1   => bA
a-1b-1 => AB
ab2    => abb
ab-2   => aBB
a-1b2  => Abb
a-1b-2 => ABB
ab3    => abbb
a-1b3  => Abbb

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
    ()       => ()
    a3       => aaa
    a-3      => AAA
    b6       => bbbbbb
    b-6      => BBBBBB
    aba-1b-1 => abAB
    ab-1a-1b => aBAb
    bab-1a-1 => baBA
    ba-1b-1a => bABa
    a-1bab-1 => AbaB
    a-1b-1ab => ABab
    b-1aba-1 => BabA
    b-1a-1ba => BAba
Repr : a
    a     => a
    a-2   => AA
    bab-1 => baB
    b-1ab => Bab
Repr : b
    b     => b
    b-5   => BBBBB
    aba-1 => abA
    a-1ba => Aba
Repr : A
    a-1     => A
    a2      => aa
    ba-1b-1 => bAB
    b-1a-1b => BAb
Repr : B
    b-1     => B
    b5      => bbbbb
    ab-1a-1 => aBA
    a-1b-1a => ABa
Repr : bb
    b2     => bb
    b-4    => BBBB
    ab2a-1 => abbA
    a-1b2a => Abba
Repr : BB
    b-2     => BB
    b4      => bbbb
    ab-2a-1 => aBBA
    a-1b-2a => ABBa
Repr : bbb
    b3  => bbb
    b-3 => BBB
Repr : ab
    ab => ab
    ba => ba
Repr : aB
    ab-1 => aB
    b-1a => Ba
Repr : bA
    ba-1 => bA
    a-1b => Ab
Repr : AB
    a-1b-1 => AB
    b-1a-1 => BA
Repr : abb
    ab2      => abb
    b2a      => bba
    a-1b2a-1 => AbbA
Repr : aBB
    ab-2      => aBB
    b-2a      => BBa
    a-1b-2a-1 => ABBA
Repr : Abb
    a-1b2 => Abb
    b2a-1 => bbA
    ab2a  => abba
Repr : ABB
    a-1b-2 => ABB
    b-2a-1 => BBA
    ab-2a  => aBBa
    ba-1b3 => bAbbb
Repr : abbb
    ab3 => abbb
    b3a => bbba
Repr : Abbb
    a-1b3  => Abbb
    b3a-1  => bbbA
    ba-1b2 => bAbb

Total Words : 65
Total Time  : 828 ms; Total Created Words : 44555
```
and
```
G = { (), a, b, c, ab, ac, bc, ca, cb, abc, aca, acb, bca, bcb, cab, abca, abcb, acab, bcab, cabc, abcab, acabc, bcabc, abcabc }
()     => ()
a      => a
b      => b
c      => c
ab     => ab
ac     => ac
bc     => bc
ca     => ca
cb     => cb
abc    => abc
aca    => aca
acb    => acb
bca    => bca
bcb    => bcb
cab    => cab
abca   => abca
abcb   => abcb
acab   => acab
bcab   => bcab
cabc   => cabc
abcab  => abcab
acabc  => acabc
bcabc  => bcabc
abcabc => abcabc

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
    ()     => ()
    a2     => aa
    b2     => bb
    c2     => cc
    a-2    => AA
    b-2    => BB
    c-2    => CC
    abab   => abab
    baba   => baba
    acacac => acacac
    bcbcbc => bcbcbc
    cacaca => cacaca
    cbcbcb => cbcbcb
Repr : a
    a     => a
    a-1   => A
    bab   => bab
    cacac => cacac
Repr : b
    b     => b
    b-1   => B
    aba   => aba
    cbcbc => cbcbc
Repr : c
    c     => c
    c-1   => C
    acaca => acaca
    bcbcb => bcbcb
Repr : ab
    ab => ab
    ba => ba
Repr : ac
    ac   => ac
    caca => caca
Repr : bc
    bc   => bc
    cbcb => cbcb
Repr : ca
    ca   => ca
    acac => acac
Repr : cb
    cb   => cb
    bcbc => bcbc
Repr : abc
    abc => abc
Repr : aca
    aca => aca
    cac => cac
Repr : acb
    acb => acb
Repr : bca
    bca => bca
Repr : bcb
    bcb => bcb
    cbc => cbc
Repr : cab
    cab => cab
Repr : abca
    abca => abca
Repr : abcb
    abcb => abcb
Repr : acab
    acab => acab
Repr : bcab
    bcab => bcab
Repr : cabc
    cabc   => cabc
    acabcb => acabcb
    bcabca => bcabca
Repr : abcab
    abcab => abcab
Repr : acabc
    acabc => acabc
    cabcb => cabcb
Repr : bcabc
    bcabc => bcabc
    cabca => cabca
Repr : abcabc
    abcabc => abcabc

Total Words : 56
Total Time  : 2079 ms; Total Created Words : 98126
```
