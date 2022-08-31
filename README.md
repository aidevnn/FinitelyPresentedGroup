# FinitelyPresentedGroup
Bruteforce algorithm for creating all elements of a group presented by generators and relations. This current version runs very slow even for smallest groups.

For example, it takes ~4000ms for generating Frobenius group $F_{20}$.
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
WordGroup.Generate("a2", "b2", "abab"); // Klein

output :

G = { (), a, b, ab }
Order      : 4
Is Group   : True
Is Abelian : True

Table
()  a  b ab
 a () ab  b
 b ab ()  a
ab  b  a ()

Classes
    () => { (), aa, bb, AA, BB, abab, baba }
    a  => { a, A, bab }
    b  => { b, B, aba }
    ab => { ab, ba }

Total Words : 15
Total Time  : 10 ms; Total Created Words : 1778
```
and
```
WordGroup.Generate("a3", "b2", "aba-1b-1"); // C6

output :

G = { (), a, b, A, ab, bA }
Order      : 6
Is Group   : True
Is Abelian : True

Table
()  a  b  A ab bA
 a  A ab () bA  b
 b ab () bA  a  A
 A () bA  a  b ab
ab bA  a  b  A ()
bA  b  A ab ()  a

Classes
    () => { (), bb, BB, aaa, AAA, abAb, babA, bAba, Abab }
    a  => { a, AA, bab }
    b  => { b, B, abA, Aba }
    A  => { A, aa, bAb }
    ab => { ab, ba }
    bA => { bA, Ab }

Total Words : 23
Total Time  : 31 ms; Total Created Words : 3830
```
and
```
WordGroup.Generate("a2", "b2", "ababab"); // S3

output :

G = { (), a, b, ab, ba, aba }
Order      : 6
Is Group   : True
Is Abelian : False

Table
 ()   a   b  ab  ba aba
  a  ()  ab   b aba  ba
  b  ba  () aba   a  ab
 ab aba   a  ba  ()   b
 ba   b aba  ()  ab   a
aba  ab  ba   a   b  ()

Classes
    ()  => { (), aa, bb, AA, BB, ababab, bababa }
    a   => { a, A, babab }
    b   => { b, B, ababa }
    ab  => { ab, baba }
    ba  => { ba, abab }
    aba => { aba, bab }

Total Words : 19
Total Time  : 27 ms; Total Created Words : 3207
```
and
```
WordGroup.Generate("a4", "a2b-2", "b-1aba"); // H8

output :

G = { (), a, b, A, B, aa, ab, ba }
Order      : 8
Is Group   : True
Is Abelian : False

Table
()  a  b  A  B aa ab ba
 a aa ab () ba  A  B  b
 b ba aa ab ()  B  a  A
 A () ba aa ab  a  b  B
 B ab () ba aa  b  A  a
aa  A  B  a  b () ba ab
ab  b  A  B  a ba aa ()
ba  B  a  b  A ab () aa

Classes
    () => { (), aaaa, AAAA, aabb, bbaa, aaBB, BBaa, baab, aBBa, AbbA, BaaB, abaB, babA, aBAB, bABA }
    a  => { a, AAA, Abb, bbA, bab, bAB }
    b  => { b, BBB, Baa, aaB, aba, aBA }
    A  => { A, aaa, aBB, BBa, baB }
    B  => { B, bbb, baa, aab, abA, ABA }
    aa => { aa, bb, AA, BB }
    ab => { ab, bA, Ba, AB, bbba }
    ba => { ba, aB, Ab, BA, abbb }

Total Words : 52
Total Time  : 256 ms; Total Created Words : 35401
```
and
```
WordGroup.Generate("a3", "b6", "ab = ba"); // C3 x C6

output :

G = { (), a, b, A, B, bb, BB, bbb, ab, aB, bA, AB, abb, aBB, Abb, ABB, abbb, Abbb }
Order      : 18
Is Group   : True
Is Abelian : True

Table
  ()    a    b    A    B   bb   BB  bbb   ab   aB   bA   AB  abb  aBB  Abb  ABB abbb Abbb
   a    A   ab   ()   aB  abb  aBB abbb   bA   AB    b    B  Abb  ABB   bb   BB Abbb  bbb
   b   ab   bb   bA   ()  bbb    B   BB  abb    a  Abb    A abbb   aB Abbb   AB  aBB  ABB
   A   ()   bA    a   AB  Abb  ABB Abbb    b    B   ab   aB   bb   BB  abb  aBB  bbb abbb
   B   aB   ()   AB   BB    b  bbb   bb    a  aBB    A  ABB   ab abbb   bA Abbb  abb  Abb
  bb  abb  bbb  Abb    b   BB   ()    B abbb   ab Abbb   bA  aBB    a  ABB    A   aB   AB
  BB  aBB    B  ABB  bbb   ()   bb    b   aB abbb   AB Abbb    a  abb    A  Abb   ab   bA
 bbb abbb   BB Abbb   bb    B    b   ()  aBB  abb  ABB  Abb   aB   ab   AB   bA    a    A
  ab   bA  abb    b    a abbb   aB  aBB  Abb    A   bb   () Abbb   AB  bbb    B  ABB   BB
  aB   AB    a    B  aBB   ab abbb  abb    A  ABB   ()   BB   bA Abbb    b  bbb  Abb   bb
  bA    b  Abb   ab    A Abbb   AB  ABB   bb   ()  abb    a  bbb    B abbb   aB   BB  aBB
  AB    B    A   aB  ABB   bA Abbb  Abb   ()   BB    a  aBB    b  bbb   ab abbb   bb  abb
 abb  Abb abbb   bb   ab  aBB    a   aB Abbb   bA  bbb    b  ABB    A   BB   ()   AB    B
 aBB  ABB   aB   BB abbb    a  abb   ab   AB Abbb    B  bbb    A  Abb   ()   bb   bA    b
 Abb   bb Abbb  abb   bA  ABB    A   AB  bbb    b abbb   ab   BB   ()  aBB    a    B   aB
 ABB   BB   AB  aBB Abbb    A  Abb   bA    B  bbb   aB abbb   ()   bb    a  abb    b   ab
abbb Abbb  aBB  bbb  abb   aB   ab    a  ABB  Abb   BB   bb   AB   bA    B    b    A   ()
Abbb  bbb  ABB abbb  Abb   AB   bA    A   BB   bb  aBB  abb    B    b   aB   ab   ()    a

Classes
    ()   => { (), aaa, AAA, bbbbbb, BBBBBB, abAB, aBAb, baBA, bABa, AbaB, ABab, BabA, BAba }
    a    => { a, AA, baB, Bab }
    b    => { b, BBBBB, abA, Aba }
    A    => { A, aa, bAB, BAb }
    B    => { B, bbbbb, aBA, ABa }
    bb   => { bb, BBBB, abbA, Abba }
    BB   => { BB, bbbb, aBBA, ABBa }
    bbb  => { bbb, BBB }
    ab   => { ab, ba }
    aB   => { aB, Ba }
    bA   => { bA, Ab }
    AB   => { AB, BA }
    abb  => { abb, bba, AbbA }
    aBB  => { aBB, BBa, ABBA }
    Abb  => { Abb, bbA, abba }
    ABB  => { ABB, BBA, aBBa, bAbbb }
    abbb => { abbb, bbba }
    Abbb => { Abbb, bbbA, bAbb }

Total Words : 65
Total Time  : 347 ms; Total Created Words : 42769
```
and
```
WordGroup.Generate("a2", "b2", "c2", "bcbcbc", "acacac", "abab"); // S4

output :

G = { (), a, b, c, ab, ac, bc, ca, cb, abc, aca, acb, bca, bcb, cab, abca, abcb, acab, bcab, cabc, abcab, acabc, bcabc, abcabc }
Order      : 24
Is Group   : True
Is Abelian : False

Table
    ()      a      b      c     ab     ac     bc     ca     cb    abc    aca    acb    bca    bcb    cab   abca   abcb   acab   bcab   cabc  abcab  acabc  bcabc abcabc
     a     ()     ab     ac      b      c    abc    aca    acb     bc     ca     cb   abca   abcb   acab    bca    bcb    cab  abcab  acabc   bcab   cabc abcabc  bcabc
     b     ab     ()     bc      a    abc      c    bca    bcb     ac   abca   abcb     ca     cb   bcab    aca    acb  abcab    cab  bcabc   acab abcabc   cabc  acabc
     c     ca     cb     ()    cab    aca    bcb      a      b   cabc     ac   acab   bcab     bc     ab  bcabc  acabc    acb    bca    abc abcabc   abcb   abca  abcab
    ab      b      a    abc     ()     bc     ac   abca   abcb      c    bca    bcb    aca    acb  abcab     ca     cb   bcab   acab abcabc    cab  bcabc  acabc   cabc
    ac    aca    acb      a   acab     ca   abcb     ()     ab  acabc      c    cab  abcab    abc      b abcabc   cabc     cb   abca     bc  bcabc    bcb    bca   bcab
    bc    bca    bcb      b   bcab   abca     cb     ab     ()  bcabc    abc  abcab    cab      c      a   cabc abcabc   abcb     ca     ac  acabc    acb    aca   acab
    ca      c    cab    aca     cb     ()   cabc     ac   acab    bcb      a      b  bcabc  acabc    acb   bcab     bc     ab abcabc   abcb    bca    abc  abcab   abca
    cb    cab      c    bcb     ca   cabc     ()   bcab     bc    aca  bcabc  acabc      a      b    bca     ac   acab abcabc     ab   abca    acb  abcab    abc   abcb
   abc   abca   abcb     ab  abcab    bca    acb      b      a abcabc     bc   bcab   acab     ac     ()  acabc  bcabc    bcb    aca      c   cabc     cb     ca    cab
   aca     ac   acab     ca    acb      a  acabc      c    cab   abcb     ()     ab abcabc   cabc     cb  abcab    abc      b  bcabc    bcb   abca     bc   bcab    bca
   acb   acab     ac   abcb    aca  acabc      a  abcab    abc     ca abcabc   cabc     ()     ab   abca      c    cab  bcabc      b    bca     cb   bcab     bc    bcb
   bca     bc   bcab   abca    bcb      b  bcabc    abc  abcab     cb     ab     ()   cabc abcabc   abcb    cab      c      a  acabc    acb     ca     ac   acab    aca
   bcb   bcab     bc     cb    bca  bcabc      b    cab      c   abca   cabc abcabc     ab     ()     ca    abc  abcab  acabc      a    aca   abcb   acab     ac    acb
   cab     cb     ca   cabc      c    bcb    aca  bcabc  acabc     ()   bcab     bc     ac   acab abcabc      a      b    bca    acb  abcab     ab   abca   abcb    abc
  abca    abc  abcab    bca   abcb     ab abcabc     bc   bcab    acb      b      a  acabc  bcabc    bcb   acab     ac     ()   cabc     cb    aca      c    cab     ca
  abcb  abcab    abc    acb   abca abcabc     ab   acab     ac    bca  acabc  bcabc      b      a    aca     bc   bcab   cabc     ()     ca    bcb    cab      c     cb
  acab    acb    aca  acabc     ac   abcb     ca abcabc   cabc      a  abcab    abc      c    cab  bcabc     ()     ab   abca     cb   bcab      b    bca    bcb     bc
  bcab    bcb    bca  bcabc     bc     cb   abca   cabc abcabc      b    cab      c    abc  abcab  acabc     ab     ()     ca   abcb   acab      a    aca    acb     ac
  cabc  bcabc  acabc    cab abcabc   bcab   acab     cb     ca  abcab    bcb    bca    acb    aca      c   abcb   abca     bc     ac     ()    abc      b      a     ab
 abcab   abcb   abca abcabc    abc    acb    bca  acabc  bcabc     ab   acab     ac     bc   bcab   cabc      b      a    aca    bcb    cab     ()     ca     cb      c
 acabc abcabc   cabc   acab  bcabc  abcab    cab    acb    aca   bcab   abcb   abca     cb     ca     ac    bcb    bca    abc      c      a     bc     ab     ()      b
 bcabc   cabc abcabc   bcab  acabc    cab  abcab    bcb    bca   acab     cb     ca   abcb   abca     bc    acb    aca      c    abc      b     ac     ()     ab      a
abcabc  acabc  bcabc  abcab   cabc   acab   bcab   abcb   abca    cab    acb    aca    bcb    bca    abc     cb     ca     ac     bc     ab      c      a      b     ()

Classes
    ()     => { (), aa, bb, cc, AA, BB, CC, abab, baba, acacac, bcbcbc, cacaca, cbcbcb }
    a      => { a, A, bab, cacac }
    b      => { b, B, aba, cbcbc }
    c      => { c, C, acaca, bcbcb }
    ab     => { ab, ba }
    ac     => { ac, caca }
    bc     => { bc, cbcb }
    ca     => { ca, acac }
    cb     => { cb, bcbc }
    abc    => { abc }
    aca    => { aca, cac }
    acb    => { acb }
    bca    => { bca }
    bcb    => { bcb, cbc }
    cab    => { cab }
    abca   => { abca }
    abcb   => { abcb }
    acab   => { acab }
    bcab   => { bcab }
    cabc   => { cabc, acabcb, bcabca }
    abcab  => { abcab }
    acabc  => { acabc, cabcb }
    bcabc  => { bcabc, cabca }
    abcabc => { abcabc }

Total Words : 56
Total Time  : 623 ms; Total Created Words : 94273
```

## More working Examples
```
WordGroup.Generate("a6"); // C6
WordGroup.Generate("a4", "b2", "abab"); // D4
WordGroup.Generate("a2", "b3", "ababab"); // A4
WordGroup.Generate("a2", "b3", "ab-1ab"); // C6
WordGroup.Generate("a4", "b3", "aba-1b");
WordGroup.Generate("a4", "b3", "abab");
WordGroup.Generate("a2", "b2", "c3", "abab", "bc=ca"); // S4
WordGroup.Generate("a2", "b2", "c2", "abcbc"); // D4
WordGroup.Generate("a6", "b4", "abab-1", "a3b2"); // H12
WordGroup.Generate("a5", "b4", "abababab", "a2ba-1b-1"); // F20
WordGroup.Generate("a4", "b4", "abab-1");
WordGroup.Generate("a2", "b2", "c2", "abab", "acac", "bcbc"); // K8
WordGroup.Generate("a2", "b2", "c3", "abab", "acac", "bc=cb");
```
