# FinitelyPresentedGroup
Bruteforce algorithm for creating all elements of a group presented by generators and relations. This current version runs very slow even for smallest groups.

For example, it takes ~600ms for generating Symm4 group of permutations.
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
    () => { (), a2, b2, a-2, b-2, abab, baba }
    a  => { a, a-1, bab }
    b  => { b, b-1, aba }
    ab => { ab, ba }

Total Words : 15
Total Time  : 10 ms; Total Created Words : 1817
```
and
```
WordGroup.Generate("a3", "b2", "aba-1b-1"); // C6

output :

G = { (), a, b, a2, ab, ba2 }
Order      : 6
Is Group   : True
Is Abelian : True

Table
 ()   a   b  a2  ab ba2
  a  a2  ab  () ba2   b
  b  ab  () ba2   a  a2
 a2  () ba2   a   b  ab
 ab ba2   a   b  a2  ()
ba2   b  a2  ab  ()   a

Classes
    ()  => { (), b2, b-2, a3, a-3, aba-1b, baba-1, ba-1ba, a-1bab, aba2b, baba2, ba2ba, a2bab }
    a   => { a, a-2, bab }
    b   => { b, b-1, aba-1, a-1ba, aba2, a2ba }
    a2  => { a-1, a2, ba-1b, ba2b }
    ab  => { ab, ba }
    ba2 => { ba-1, a-1b, ba2, a2b }

Total Words : 23
Total Time  : 26 ms; Total Created Words : 3945
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
    ()  => { (), a2, b2, a-2, b-2, ababab, bababa }
    a   => { a, a-1, babab }
    b   => { b, b-1, ababa }
    ab  => { ab, baba }
    ba  => { ba, abab }
    aba => { aba, bab }

Total Words : 19
Total Time  : 19 ms; Total Created Words : 3274
```
and
```
WordGroup.Generate("a4", "a2b-2", "b-1aba"); // H8

output :

G = { (), a, b, a2, a3, b3, ab, ab3 }
Order      : 8
Is Group   : True
Is Abelian : False

Table
 ()   a   b  a2  a3  b3  ab ab3
  a  a2  ab  a3  () ab3  b3   b
  b ab3  a2  b3  ab  ()   a  a3
 a2  a3  b3  ()   a   b ab3  ab
 a3  () ab3   a  a2  ab   b  b3
 b3  ab  ()   b ab3  a2  a3   a
 ab   b  a3 ab3  b3   a  a2  ()
ab3  b3   a  ab   b  a3  ()  a2

Classes
    ()  => { (), a4, a-4, a2b2, a2b-2, b2a2, b-2a2, ab2a, ab-2a, ba2b, a-1b2a-1, b-1a2b-1, a3b2a3, b3a2b3, abab-1, ab-1a-1b-1, baba-1, ba-1b-1a-1, abab3, baba3, ab3a3b3, ba3b3a3 }
    a   => { a, a-3, a-1b2, b2a-1, b2a3, a3b2, ba-1b-1, ba3b3 }
    b   => { b, b-3, b-1a2, a2b-1, a2b3, b3a2, aba, ab-1a-1, ab3a3 }
    a3  => { a-1, a3, ab2, ab-2, b2a, b-2a, bab-1, bab3 }
    b3  => { b-1, b3, ba2, a2b, aba-1, a-1b-1a-1, aba3, a3b3a3 }
    a2  => { a2, b2, a-2, b-2 }
    ab  => { ab, ba-1, a-1b-1, b-1a, ba3, b3a, b-3a-1, a3b3 }
    ab3 => { ab-1, ba, a-1b, b-1a-1, ab3, a3b, b3a3 }

Total Words : 52
Total Time  : 205 ms; Total Created Words : 35483
```
and
```
WordGroup.Generate("a3", "b6", "ab = ba"); // C3 x C6

output :

G = { (), a, b, a2, b2, b3, b4, b5, ab, ab2, ba2, ab3, a2b2, ab4, a2b3, ab5, a2b4, a2b5 }
Order      : 18
Is Group   : True
Is Abelian : True

Table
  ()    a    b   a2   b2   b3   b4   b5   ab  ab2  ba2  ab3 a2b2  ab4 a2b3  ab5 a2b4 a2b5
   a   a2   ab   ()  ab2  ab3  ab4  ab5  ba2 a2b2    b a2b3   b2 a2b4   b3 a2b5   b4   b5
   b   ab   b2  ba2   b3   b4   b5   ()  ab2  ab3 a2b2  ab4 a2b3  ab5 a2b4    a a2b5   a2
  a2   ()  ba2    a a2b2 a2b3 a2b4 a2b5    b   b2   ab   b3  ab2   b4  ab3   b5  ab4  ab5
  b2  ab2   b3 a2b2   b4   b5   ()    b  ab3  ab4 a2b3  ab5 a2b4    a a2b5   ab   a2  ba2
  b3  ab3   b4 a2b3   b5   ()    b   b2  ab4  ab5 a2b4    a a2b5   ab   a2  ab2  ba2 a2b2
  b4  ab4   b5 a2b4   ()    b   b2   b3  ab5    a a2b5   ab   a2  ab2  ba2  ab3 a2b2 a2b3
  b5  ab5   () a2b5    b   b2   b3   b4    a   ab   a2  ab2  ba2  ab3 a2b2  ab4 a2b3 a2b4
  ab  ba2  ab2    b  ab3  ab4  ab5    a a2b2 a2b3   b2 a2b4   b3 a2b5   b4   a2   b5   ()
 ab2 a2b2  ab3   b2  ab4  ab5    a   ab a2b3 a2b4   b3 a2b5   b4   a2   b5  ba2   ()    b
 ba2    b a2b2   ab a2b3 a2b4 a2b5   a2   b2   b3  ab2   b4  ab3   b5  ab4   ()  ab5    a
 ab3 a2b3  ab4   b3  ab5    a   ab  ab2 a2b4 a2b5   b4   a2   b5  ba2   () a2b2    b   b2
a2b2   b2 a2b3  ab2 a2b4 a2b5   a2  ba2   b3   b4  ab3   b5  ab4   ()  ab5    b    a   ab
 ab4 a2b4  ab5   b4    a   ab  ab2  ab3 a2b5   a2   b5  ba2   () a2b2    b a2b3   b2   b3
a2b3   b3 a2b4  ab3 a2b5   a2  ba2 a2b2   b4   b5  ab4   ()  ab5    b    a   b2   ab  ab2
 ab5 a2b5    a   b5   ab  ab2  ab3  ab4   a2  ba2   () a2b2    b a2b3   b2 a2b4   b3   b4
a2b4   b4 a2b5  ab4   a2  ba2 a2b2 a2b3   b5   ()  ab5    b    a   b2   ab   b3  ab2  ab3
a2b5   b5   a2  ab5  ba2 a2b2 a2b3 a2b4   ()    b    a   b2   ab   b3  ab2   b4  ab3  ab4

Classes
    ()   => { (), a3, a-3, b6, b-6, aba-1b-1, ab-1a-1b, bab-1a-1, ba-1b-1a, a-1bab-1, a-1b-1ab, b-1aba-1, b-1a-1ba, aba2b5, ab5a2b, bab5a2, ba2b5a, a2bab5, a2b5ab, b5aba2, b5a2ba }
    a    => { a, a-2, bab-1, b-1ab, bab5, b5ab }
    b    => { b, b-5, aba-1, a-1ba, aba2, a2ba }
    a2   => { a-1, a2, ba-1b-1, b-1a-1b, ba2b5, b5a2b }
    b5   => { b-1, b5, ab-1a-1, a-1b-1a, ab5a2, a2b5a }
    b2   => { b2, b-4, ab2a-1, a-1b2a, ab2a2, a2b2a }
    b4   => { b-2, b4, ab-2a-1, a-1b-2a, ab4a2, a2b4a }
    b3   => { b3, b-3 }
    ab   => { ab, ba }
    ab5  => { ab-1, b-1a, ab5, b5a }
    ba2  => { ba-1, a-1b, ba2, a2b }
    a2b5 => { a-1b-1, b-1a-1, a2b5, b5a2 }
    ab2  => { ab2, b2a, a-1b2a-1, a2b2a2 }
    ab4  => { ab-2, b-2a, ab4, b4a, a-1b-2a-1, a2b4a2 }
    a2b2 => { a-1b2, b2a-1, a2b2, b2a2, ab2a }
    a2b4 => { a-1b-2, b-2a-1, a2b4, b4a2, ab-2a, ba-1b3, ab4a, ba2b3 }
    ab3  => { ab3, b3a }
    a2b3 => { a-1b3, b3a-1, a2b3, b3a2, ba-1b2, ba2b2 }

Total Words : 65
Total Time  : 355 ms; Total Created Words : 44532
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
    ()     => { (), a2, b2, c2, a-2, b-2, c-2, abab, baba, acacac, bcbcbc, cacaca, cbcbcb }
    a      => { a, a-1, bab, cacac }
    b      => { b, b-1, aba, cbcbc }
    c      => { c, c-1, acaca, bcbcb }
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
Total Time  : 558 ms; Total Created Words : 95049
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