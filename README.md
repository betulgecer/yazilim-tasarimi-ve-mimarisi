# yazilim-tasarimi-ve-mimarisi
## Observe Tasarım Deseni
Observer tasarım deseni behavioral (davranışsal) grubuna aittir.  


Gözlemci ( Observer ) Tasarım Deseni Nedir?

Bir nesnenin durumlarında değişiklik olduğunda, bu değişikliklerden haberdar olmak isteyen diğer nesnelere haber verilmesi gerektiği durumlarda bu tasarım deseni kullanılır. Bu haber verilme işlemi sırasında, haber verilecek nesnelerin birbirlerine bağımlı olması istenmez. Yani kısaca dinleyici konumunda bulunan bir çok nesne, bir nesnenin durumunu sürekli gözlemler. Bir değişiklik sırasında gözlemcilere haber verilir.


Observer Tasarım Desenindeki Amaç:

Tasarlanmış olan sistem içerisinde, değişimini izlemek istediğimiz bir değer için kullanılır.


Observer Pattern Ne Zaman Kullanılır?  

-Bir nesneyi değiştirdiğinizde birden fazla nesnenin değişmesi gerekliyse ve kaç tane nesnenin değişeceğinden habersizseniz.  
-Eğer bir soyutlamanın birbirine bağlı iki görünümü varsa ve bu sınıfları birden fazla sınıfa ayırmak sonradan kullanımlarını kolaylaştırıyorsa.  
 -Eğer bir nesnenin diğer nesnelerin hangi nesneler olduğundan habersiz, onları haberdar etme ihtiyacı varsa ve bu nesneyle diğer nesneleri birbirine kuvvetli bağlarla bağlamak istemiyorsanız.


Observer Pattern Nerelerde Kullanılır?   

Örneğin bir alışveriş sitesinde bir ürüne indirim yapıldığında kullanıcılarınıza e-mail ile haber verilir iken bu kalıp kullanılabilir. Ya da en basitinden facebook da bir gruba üyesiniz grupta bildirimleri açtığınızda size (ve daha birçok kişiye) gelecek olan bildirim bu yapı ile olabilir.

Faydaları Nedir?

Loosely-coupled uygulamalar yapmayı sağlar. Subject ile Observer birbirleriyle loosely-coupled'tır.  
Bir nesnenin birden çok nesneyi otomatik olarak etkilemesini istiyorsak bu tasarım desenini kullanabiliriz. Örneğin, uygulamamızda A ve B kısımları olsun. A kısmında anlık sıcaklığın gösterildiğini varsayalım. B kısmı ise sıcaklık 20 derecenin altında olduğu zaman yeşil bir ışık göstersin. B kısmının sıcaklık değişikliklerine tepki göstermesi için kendisini A kısmının dinleyicisi(listener) olarak kaydetmesi gerekir. Kaydettikten sonra her bir sıcaklık değişimini izleyerek yeşil ışık gösterip göstermeyeceğini otomatik olarak kontrol eder.  


Observer Design Pattern’de;  
Subject: Takip edilecek olan nesnemiz.  
Observer: Abstract gözlemci sınıfımız. Soyutlamanın sebebi ise birden fazla gözlemci tarafından takip edilebilmesini sağlamak.  
ConcreteObserver: Gerçek takip eden nesnemiz.


![Imega of Class](https://github.com/betulgecer/yazilim-tasarimi-ve-mimarisi/blob/master/observerUML.png) 

Kodlar

Observer Interface
```java
public interface Observer {
    public void update(float temp, float humidity, float pressure);
}
```
Observer interface içerisinde sadece tek bir tane metod bulunması yeterlidir. Burada update() isimli metod kullanıldı.

Subject Interface
```java
public interface Subject {
    public void registerObserver(Observer o);
    public void removeObserver(Observer o);
    public void notifyObservers();
}
```
Subject interface içerisinde bulunan tüm metodlar zorunludur. Bunların dışında başka metodlar eklenebilir.

DisplayElement Interface
```java	
public interface DisplayElement {
    public void display();
}
```
Somut Subject Sınıfı
```java	
public class WeatherData implements Subject {
    private ArrayList observers;
    private float temperature;
    private float humidity;
    private float pressure;
           
    public WeatherData() {
        observers = new ArrayList();
    }
           
    public void registerObserver(Observer o) {
        observers.add(o);
    }
           
    public void removeObserver(Observer o) {
        int i = observers.indexOf(o);
        if (i >= 0) {
            observers.remove(i);
        }
    }
           
    public void notifyObservers() {
        for (int i = 0; i < observers.size(); i++) {
            Observer observer = (Observer)observers.get(i);
            observer.update(temperature, humidity, pressure);
        }
    }
           
    public void measurementsChanged() {
        notifyObservers();
    }
           
    public void setMeasurements(float temperature, float humidity, float pressure) {
        this.temperature = temperature;
        this.humidity = humidity;
        this.pressure = pressure;
        measurementsChanged();
    }
           
    public float getTemperature() {
        return temperature;
    }
           
    public float getHumidity() {
        return humidity;
    }
           
    public float getPressure() {
        return pressure;
    }
}
```  
WeatherData sınıfı Observer tasarım deseninde somut Subject sınıfını temsil eder. 


Somut Observer Sınıfları
```java
public class ForecastDisplay implements Observer, DisplayElement {
    private float currentPressure = 29.92f;  
    private float lastPressure;
    private WeatherData weatherData;
       
    public ForecastDisplay(WeatherData weatherData) {
        this.weatherData = weatherData;
        weatherData.registerObserver(this);
    }
       
    public void update(float temp, float humidity, float pressure) {
                lastPressure = currentPressure;
        currentPressure = pressure;
       
        display();
    }
       
    public void display() {
        System.out.print("Forecast: ");
        if (currentPressure > lastPressure) {
            System.out.println("Improving weather on the way!");
        } else if (currentPressure == lastPressure) {
            System.out.println("More of the same");
        } else if (currentPressure < lastPressure) {
            System.out.println("Watch out for cooler, rainy weather");
        }
    }
}
```
Somut Observer sınıfı. Görüldüğü gibi constructor parametre olarak somut Subject türünden bir nesne alıyor ve bu nesnenin registerObserver() metodunu kullanarak kendisini kaydediyor. update() metodunda ise Subject sınıfının yaptığı değişikliği alıyor ve işliyor. 

İkinci Somut Observer sınıfı.
```java
public class StatisticsDisplay implements Observer, DisplayElement {
    private float maxTemp = 0.0f;
    private float minTemp = 200;
    private float tempSum= 0.0f;
    private int numReadings;
    private WeatherData weatherData;
       
    public StatisticsDisplay(WeatherData weatherData) {
        this.weatherData = weatherData;
        weatherData.registerObserver(this);
    }
       
    public void update(float temp, float humidity, float pressure) {
        tempSum += temp;
        numReadings++;
       
        if (temp > maxTemp) {
            maxTemp = temp;
        }
        
        if (temp < minTemp) {
            minTemp = temp;
        }
       
        display();
    }
       
    public void display() {
        System.out.println("Avg/Max/Min temperature = " + (tempSum / numReadings)
            + "/" + maxTemp + "/" + minTemp);
    }
}
```

Test Sınıfı
```java
public class WeatherStation {
       
    public static void main(String[] args) {
        WeatherData weatherData = new WeatherData();
       
        StatisticsDisplay statisticsDisplay = new StatisticsDisplay(weatherData);
        ForecastDisplay forecastDisplay = new ForecastDisplay(weatherData);
       
        weatherData.setMeasurements(80, 65, 30.4f);
        weatherData.setMeasurements(82, 70, 29.2f);
        weatherData.setMeasurements(78, 90, 29.2f);
    }
}
```
Test sınıfı. Dikkat edersek, observer sınıflarından nesne yaratılırken, parametre olarak somut Subject sınıfı kullanıldı.




