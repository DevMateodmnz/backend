# TP Caso de Estudio – Cajero Automático

El caso pide analizar las clases necesarias para que un cajero autentique usuarios, consulte saldos, retire efectivo y reciba depósitos.

## Clase: CajeroAutomatico

Coordina la sesión del usuario y las transacciones del cajero.

### Atributos

- `numeroCuentaUsuario : int`
- `usuarioAutenticado : bool`
- `pantalla : Pantalla`
- `teclado : Teclado`
- `dispensadorEfectivo : DispensadorEfectivo`
- `ranuraDeposito : RanuraDeposito`
- `baseDeDatosBanco : BaseDeDatosBanco`

### Métodos

- `Ejecutar()`
- `AutenticarUsuario()`
- `MostrarMenuPrincipal()`
- `RealizarTransaccion()`

## Clase: Cuenta

Representa la cuenta bancaria de un usuario.

### Atributos

- `numeroCuenta : int`
- `nip : int`
- `saldoDisponible : decimal`
- `saldoTotal : decimal`

El saldo disponible no incluye un depósito hasta que el banco lo verifique.

### Métodos

- `ValidarNip(int nip)`
- `ObtenerSaldoDisponible()`
- `ObtenerSaldoTotal()`
- `Acreditar(decimal monto)`
- `Debitar(decimal monto)`

## Clase: BaseDeDatosBanco

Guarda las cuentas y permite consultar o modificar sus datos.

### Atributos

- `cuentas : List<Cuenta>`

### Métodos

- `AutenticarUsuario(int numeroCuenta, int nip)`
- `ObtenerSaldoDisponible(int numeroCuenta)`
- `ObtenerSaldoTotal(int numeroCuenta)`
- `Acreditar(int numeroCuenta, decimal monto)`
- `Debitar(int numeroCuenta, decimal monto)`

## Clase: Pantalla

Muestra los mensajes y montos al usuario.

### Atributos

- No necesita atributos propios para este análisis.

### Métodos

- `MostrarMensaje(string mensaje)`
- `MostrarMonto(decimal monto)`

## Clase: Teclado

Recibe los números ingresados por el usuario.

### Atributos

- No necesita atributos propios para este análisis.

### Métodos

- `ObtenerEntrada()`

## Clase: DispensadorEfectivo

Entrega el efectivo cuando un retiro fue autorizado.

### Atributos

- `cantidadBilletes : int`
- `valorBillete : decimal`

El caso indica que comienza con 500 billetes de $20.

### Métodos

- `HayEfectivoSuficiente(decimal monto)`
- `DispensarEfectivo(decimal monto)`

## Clase: RanuraDeposito

Recibe el sobre que entrega el usuario al hacer un depósito.

### Atributos

- No necesita atributos propios para este análisis.

### Métodos

- `RecibirSobre()`

## Clase: Transaccion

Es una clase abstracta que reúne los datos comunes de las operaciones del cajero.

### Atributos

- `numeroCuenta : int`
- `pantalla : Pantalla`
- `baseDeDatosBanco : BaseDeDatosBanco`

### Métodos

- `Ejecutar()`

## Clase: ConsultaSaldo

Consulta y muestra los saldos de una cuenta.

### Atributos

- Hereda los atributos de `Transaccion`.

### Métodos

- `Ejecutar()`

## Clase: Retiro

Permite seleccionar un monto, verificar el saldo y pedir efectivo al dispensador.

### Atributos

- `monto : decimal`
- `teclado : Teclado`
- `dispensadorEfectivo : DispensadorEfectivo`

### Métodos

- `Ejecutar()`

## Clase: Deposito

Solicita el monto, recibe el sobre y registra el depósito en la cuenta.

### Atributos

- `monto : decimal`
- `teclado : Teclado`
- `ranuraDeposito : RanuraDeposito`

### Métodos

- `Ejecutar()`

## Relación entre las clases

`CajeroAutomatico` utiliza la pantalla, el teclado, el dispensador, la ranura y la base de datos. La base de datos contiene las cuentas. `ConsultaSaldo`, `Retiro` y `Deposito` heredan de `Transaccion`.
